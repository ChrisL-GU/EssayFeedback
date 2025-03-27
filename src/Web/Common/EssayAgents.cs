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

     private static readonly Dictionary<string, string> agentNameMap = new()
     {
         {nameof(ApStylebookAgent), "AP Stylebook" },
         {nameof(CognitiveBiasDetectionAgent), "Cognitive Bias" },
         {nameof(ToneConsistencyAgent), "Tone Consistency" },
         {"Inclusive language", "Inclusive Language" }
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
     public Agent? GetFinalAzureAiAgent(string[] selectedAgentNames) => GetAgents()
         .LastOrDefault(agent => agent is AzureAIAgent && IsAgentInSelectedList(selectedAgentNames, agent));

     public Agent? GetFinalAgent(string[] selectedAgentNames) => GetAgents()
         .LastOrDefault(agent => agent is not AzureAIAgent && IsAgentInSelectedList(selectedAgentNames, agent));

     private static bool IsAgentInSelectedList(string[] selectedAgentNames, Agent agent) =>
         selectedAgentNames.Contains(agentNameMap.Single(map => map.Key.Equals(
                 agent.Name,
                 StringComparison.OrdinalIgnoreCase)).Value);
}