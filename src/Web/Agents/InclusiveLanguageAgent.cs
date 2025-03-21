namespace EssayFeedback.Agents;

using Azure.AI.Projects;
using Azure.Identity;
using Common;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.Agents.AzureAI;
using Agent = Microsoft.SemanticKernel.Agents.Agent;

public static class InclusiveLanguageAgent
{
    private const string agentId = "asst_8vFIQwSPGOmVBKA0c6G74FHA";
    public static Agent CreateAgent(Kernel kernel, Settings settings)
    {
        var client = new AgentsClient(
            settings.AzureAiSettings.AgentSettings.ConnectionString,
            new DefaultAzureCredential());
        
        var agent = (client.GetAgentAsync(agentId)).GetAwaiter().GetResult().Value;
        var aiAgent = new AzureAIAgent(agent, client)
        {
            Kernel = kernel
        };
        return aiAgent;
    }
}