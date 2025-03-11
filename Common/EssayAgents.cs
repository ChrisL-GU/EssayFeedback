namespace EssayFeedback.Common;

using Agents;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.Agents;

public class EssayAgents
{
     private readonly ChatCompletionAgent apStylebookAgent;
     private readonly ChatCompletionAgent toneConsistencyAgent;
     private readonly ChatCompletionAgent biasDetectionAgent;
 
     public EssayAgents(Kernel kernel)
     {
         apStylebookAgent = ApStylebookAgent.CreateAgent(kernel);
         toneConsistencyAgent = ToneConsistencyAgent.CreateAgent(kernel);
         biasDetectionAgent = BiasDetectionAgent.CreateAgent(kernel);
     }
     
     public Agent[] GetAgents() => [apStylebookAgent, toneConsistencyAgent, biasDetectionAgent];
     public Agent InitialAgent => GetAgents().First();
     public Agent FinalAgent => GetAgents().Last();   
}