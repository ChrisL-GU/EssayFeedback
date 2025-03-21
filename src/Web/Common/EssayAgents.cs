namespace EssayFeedback.Common;

using Agents;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.Agents;
using Microsoft.SemanticKernel.Agents.AzureAI;

public class EssayAgents
{
     private readonly Dictionary<string, Agent> agents;
     private readonly Settings settings;

     private static readonly Dictionary<string, Func<Kernel, Settings, Agent>> scaffoldAgents = new()
     {
         {"AP Stylebook", ApStylebookAgent.CreateAgent },
         {"Cognitive Bias", CognitiveBiasDetectionAgent.CreateAgent },
         {"Tone Consistency", ToneConsistencyAgent.CreateAgent },
         {"Inclusive Language", InclusiveLanguageAgent.CreateAgent }
     };

     public EssayAgents(Kernel kernel, Settings settings)
     {
         this.settings = settings;
         agents = scaffoldAgents.ToDictionary(
             agent => agent.Key,
             agent => agent.Value(kernel, this.settings));
     }
     
     public Agent[] GetAgents() => agents.Values.ToArray();
     public Agent[] GetAgentsByName(string[] agentNames) =>
         agents
             .Where(agent => agentNames.Contains(agent.Key))
             .Where(agent => agent.Value is not AzureAIAgent)
             .Select(agent => agent.Value)
             .ToArray();
     
     public Agent[] GetAzureAgentsByName(string[] agentNames) =>
         agents
             .Where(agent => agentNames.Contains(agent.Key))
             .Where(agent => agent.Value is AzureAIAgent)
             .Select(agent => agent.Value)
             .ToArray();

     public static string[] GetAgentNames() => scaffoldAgents.Keys.ToArray();
     public Agent InitialAgent => GetAgents().First();
     public Agent FinalAzureAiAgent => GetAgents().Last(agent => agent is AzureAIAgent);
     
     public Agent FinalAgent => GetAgents().Last(agent => agent is not AzureAIAgent);

}