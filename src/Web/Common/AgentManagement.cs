namespace EssayFeedback.Common;

using System.Text;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.Agents;
using Microsoft.SemanticKernel.Agents.Chat;
using Strategies;

public record AgentHistoryResponse(string ModelAnalysis, string Recommendation);

public class AgentManagement(Settings settings)
{
    public GroupAgents CreateAgentGroupChatFor(string selectedModel, string[] selectedAgentNames)
    {
        var builder = Kernel.CreateBuilder();

        var modelSettings = settings
            .AzureAiSettings
            .ModelSettings
            .Single(modelSettings => modelSettings.Key == selectedModel);
        if (modelSettings.Value.IsAzureOpenAiModel) AddOpenASettings(modelSettings, builder);
        else AddAiInferenceSettings(modelSettings, builder);

        var kernel = builder.Build();
        var agents = new EssayAgents(kernel, settings);

        var finalAgent = agents.GetFinalAgent(selectedAgentNames);
        var finalAzureAiAgent = agents.GetFinalAzureAiAgent(selectedAgentNames);
        return new GroupAgents(
            new AgentGroupChat(agents.GetAgentsByName(selectedAgentNames))
            {
                ExecutionSettings = new AgentGroupChatSettings
                {
                    SelectionStrategy = new SequentialSelectionStrategy(),
                    TerminationStrategy = new ApprovalTerminationStrategy()
                    {
                        Agents = finalAgent is null ? [] : [finalAgent],
                        MaximumIterations = agents.GetAgents().Length
                    }
                }
            },
            new AgentGroupChat(agents.GetAzureAgentsByName(selectedAgentNames))
            {
                ExecutionSettings = new AgentGroupChatSettings
                {
                    SelectionStrategy = new SequentialSelectionStrategy(),
                    TerminationStrategy = new ApprovalTerminationStrategy()
                    {
                        Agents = finalAzureAiAgent is null ? [] : [finalAzureAiAgent],
                        MaximumIterations = agents.GetAgents().Length
                    }
                }
            }
        );
    }

    public async Task<AgentHistoryResponse> AggergateAgentHistoryResponses(AgentGroupChat chat)
    {
        var history = await chat.GetChatMessagesAsync().ToArrayAsync();

        var modelAnalysis = new StringBuilder();
        var modelRecommendation = new StringBuilder();
        for (var i = history.Length - 2; i >= 0; i--)
        {
            var analysis = history[i].Content?.GetBetween("<essay_analysis>", "</essay_analysis>");
            var recommendation = history[i].Content?.GetBetween("<feedback>", "</feedback>");
            
            modelAnalysis.AppendLine($"# {history[i].AuthorName}{Environment.NewLine}{analysis ?? "[No analysis provided]"}");
            modelRecommendation.AppendLine(recommendation ?? "[No feedback provided]");
        }
        
        return new AgentHistoryResponse(modelAnalysis.ToString(), modelRecommendation.ToString()); 
    }

    private void AddOpenASettings(KeyValuePair<string, ModelSettings> modelSettings, IKernelBuilder builder) =>
        builder.AddAzureOpenAIChatCompletion(
            modelSettings.Key,
            modelSettings.Value.Endpoint,
            modelSettings.Value.ApiKey);

    private void AddAiInferenceSettings(KeyValuePair<string, ModelSettings> modeliSettings, IKernelBuilder builder) =>
        builder.AddAzureAIInferenceChatCompletion(
            modeliSettings.Key,
            modeliSettings.Value.ApiKey,
            new Uri(modeliSettings.Value.Endpoint));
}
public record GroupAgents(AgentGroupChat StandardChat, AgentGroupChat AzureAiAgentChat);