namespace EssayFeedback.Common;

using Agents;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.Agents;

public class EssayAgents
{
     private readonly Dictionary<string, Agent> agents;

     private static readonly Dictionary<string, Func<Kernel, Agent>> scaffoldAgents = new()
     {
         {"AP Stylebook", ApStylebookAgent.CreateAgent },
         {"Tone Consistency", ToneConsistencyAgent.CreateAgent },
         {"Bias Detection", BiasDetectionAgent.CreateAgent }
     };

     public EssayAgents(Kernel kernel)
     {
         agents = scaffoldAgents.ToDictionary(
             agent => agent.Key,
             agent => agent.Value(kernel));
     }
     
     public Agent[] GetAgents() => agents.Values.ToArray();
     public Agent[] GetAgentsByName(string[] agentNames) =>
         agents
             .Where(agent => agentNames.Contains(agent.Key))
             .Select(agent => agent.Value)
             .ToArray();

     public static string[] GetAgentNames() => scaffoldAgents.Keys.ToArray();
     public Agent InitialAgent => GetAgents().First();
     public Agent FinalAgent => GetAgents().Last();

}