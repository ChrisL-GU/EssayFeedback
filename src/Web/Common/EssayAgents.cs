namespace EssayFeedback.Common;

using Agents;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.Agents;

public class EssayAgents
{
     private readonly Dictionary<string, Agent> agents;

     public EssayAgents()
     {
         agents = new Dictionary<string, Agent>();
     }

     public EssayAgents(Kernel kernel)
     {
         agents = new Dictionary<string, Agent>
         {
             {"AP Stylebook", ApStylebookAgent.CreateAgent(kernel)},
             {"Tone Consistency", ToneConsistencyAgent.CreateAgent(kernel)},
             {"Bias Detection", BiasDetectionAgent.CreateAgent(kernel)}
         };
     }
     
     public Agent[] GetAgents() => agents.Values.ToArray();
     public Agent[] GetAgentsByName(string[] agentNames) =>
         agents
             .Where(agent => agentNames.Contains(agent.Key))
             .Select(agent => agent.Value)
             .ToArray();
     public string[] GetAgentNames() => agents.Keys.ToArray();
     public Agent InitialAgent => GetAgents().First();
     public Agent FinalAgent => GetAgents().Last();

}