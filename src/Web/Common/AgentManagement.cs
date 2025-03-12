namespace EssayFeedback.Common;

using System.Text;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.Agents;
using Microsoft.SemanticKernel.Agents.Chat;
using Strategies;

public record AgentHistoryResponse(string ModelAnalysis, string Recommendation);

public class AgentManagement(Settings settings)
{
    public AgentGroupChat CreateAgentGroupChatFor(string selectedModel, string[] selectedAgentNames)
    {
        var builder = Kernel.CreateBuilder();
        
        if (selectedModel == "gpt-4o") AddGptSettings(selectedModel, builder);
        else AddLlamaSettings(selectedModel, builder);
            
        var kernel = builder.Build();
        var agents = new EssayAgents(kernel);
        
        return new AgentGroupChat(agents.GetAgentsByName(selectedAgentNames))
        {
            ExecutionSettings = new AgentGroupChatSettings
            {
                SelectionStrategy = new SequentialSelectionStrategy(),
                TerminationStrategy = new ApprovalTerminationStrategy()
                {
                    Agents = [agents.FinalAgent],
                    MaximumIterations = agents.GetAgents().Length
                }
            }
        };
    }

    public async Task<AgentHistoryResponse> AggergateAgentHistoryResponses(AgentGroupChat chat)
    {
        var history = await chat.GetChatMessagesAsync().ToArrayAsync();

        var modelAnalysis = new StringBuilder();
        var modelRecommendation = new StringBuilder();
        for (var i = history.Length - 1; i >= 0; i--)
        {
            var analysis = history[i].Content?.GetBetween("<essay_analysis>", "</essay_analysis>");
            var recommendation = history[i].Content?.GetBetween("<feedback>", "</feedback>");
            
            modelAnalysis.AppendLine($"# {history[i].AuthorName}{Environment.NewLine}{analysis ?? "[No analysis provided]"}");
            modelRecommendation.AppendLine(recommendation ?? "[No feedback provided]");
        }
        
        return new AgentHistoryResponse(modelAnalysis.ToString(), modelRecommendation.ToString()); 
    }

    private void AddGptSettings(string selectedModel, IKernelBuilder builder) =>
        builder.AddAzureOpenAIChatCompletion(
            selectedModel,
            settings.AzureOpenAi.EndpointGpt4o,
            settings.AzureOpenAi.ApiKeyGpt4o);

    private void AddLlamaSettings(string selectedModel, IKernelBuilder builder) =>
        builder.AddAzureAIInferenceChatCompletion(
            selectedModel,
            settings.AzureOpenAi.ApiKeyLlama,
            new Uri(settings.AzureOpenAi.EndpointLlama));
}