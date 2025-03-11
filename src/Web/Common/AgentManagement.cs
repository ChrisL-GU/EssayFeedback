namespace EssayFeedback.Common;

using System.Text;
using Microsoft.SemanticKernel.Agents;
using Microsoft.SemanticKernel.Agents.Chat;
using Strategies;

public static class AgentManagement
{
    public static AgentGroupChat CreateAgentGroupChatFor(EssayAgents agents, string[] agentNames) =>
        new(agents.GetAgentsByName(agentNames))
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

    public static async Task<AgentHistoryResponse> AggergateAgentHistoryResponses(AgentGroupChat chat)
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
    
    public record AgentHistoryResponse(string ModelAnalysis, string Recommendation);
}