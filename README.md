# Essay Feedback

This is a Blazor server web app exploring the Agents framework in Semantic Kernel. Agents are
created to review the text of a submitted essay and provide feedback to the user. Each agent focuses
on a particular aspect of the text. Agents are instructed to not rewrite the text, they are only to
identify issues and provide explanations of the issue.

> [!NOTE]
> This app is for demonstration purposes

## Demo

The demo evaulates [Paul Graham's essay Writes and Write-Nots essay][] using the AP Stylebook,
Bias Detection and Tone Consistenty Evaluator agents.

https://github.com/user-attachments/assets/c269c9f5-0f86-4824-a275-f71f8ff2c788

[Paul Graham's essay Writes and Write-Nots essay]: https://www.paulgraham.com/writes.html

## Responsible AI checklist

[Principles from Microsoft's responsible AI](https://www.microsoft.com/en-us/ai/responsible-ai)

- Fairness: The agents are guided by prompts but most of the agents rely on their own evaluation criteria, and therefore rely on their training. Therefore their recommendations are susceptible to discrimination. Some agents, such as the agent for Tone, are more at risk than other agents with more well established guidelines, such as the AP Stylebook.
- Reliability and safety: The application is meant for demonstrations and is not deployed anywhere. Therefore it is not robust and can error. The application should not be relied on for production level work.
- Privacy and security: No data is stored, either by the browser or on the server, and no data is shared with any party.
- Inclusiveness: The application relies on the training of the models to provide recommendations. The application attempts to provide a variety of evaluation criteria which will be helpful for a diverse group of people. The UI is simple, but not robust with minimal intentional accessibility built in.
- Transparency: The AI model feedback is displayed to the user while executing and can be viewed after processing has finished. AI prompts are also accessible for users to review, along with the application code, to assist in understanding reasons for recommendations which were provided.
- Accountability: No auditing or tracing currently exist for the application. Care was taken to craft prompts, but do not have evaluations.

## Agents

The currently implemented agents are:

- AP Stylebook Agent - Checks adherence to the rules set forth in the AP Stylebook
- Bias Detection Agent - Identifies potential biases in language, framing, or reasoning
- Tone Consistency Evaluator - Ensures the tone remains appropriate and consistent throughout
 
## Potential future agents

Ideas for future agents are:

- Argument Strength Analyzer - Evaluates the logical structure and persuasiveness of arguments
- Evidence Evaluator - Assesses the quality, relevance, and sufficiency of supporting evidence
- Citation Format Checker - Verifies proper formatting for various citation styles (MLA, APA, Chicago, etc.)
- Readability Assessment Agent - Measures readability scores and suggests improvements for various audience levels
- Transition Evaluator - Identifies effective/ineffective transitions between paragraphs and ideas
- Academic Vocabulary Assessor - Analyzes word choice for academic appropriateness and suggests enhancements
- Thesis Clarity Evaluator - Assesses how clearly the thesis is stated and supported throughout
- Audience Alignment Agent - Evaluates if the content matches the intended audience and purpose
- Counterargument Detector - Identifies missing or weak counterarguments that should be addressed
- Factual Accuracy Checker - Flags potential factual inaccuracies or statements requiring verification
- Grammar Proficiency Analyzer - Provides nuanced grammar feedback beyond basic spell checking
- Redundancy Inspector - Identifies repetitive content, concepts, or phrasing
- Abstract/Introduction Reviewer - Evaluates how effectively the opening frames the essay
- Conclusion Effectiveness Analyzer - Assesses how well the conclusion ties together the essay's points
- Paragraph Structure Evaluator - Checks for proper topic sentences, supporting details, and paragraph unity
- Historical/Cultural Context Advisor - Flags references that might need additional context for readers
- Technical Terminology Reviewer - Checks for appropriate use of domain-specific terminology
- Visual Element Integration Advisor - Provides feedback on the effective use of charts, graphs, or images

## Running

Deploy the following models to Azure Foundry:

- `gpt-4o` and use the name "GPT-4o"
- `Llama-3.3-70B-Instruct` and use the name "Llama-3.3-70B-Instruct"
- `Ministral-3b` and use the name "Ministral-3b"

### Locally

Add the `dotnet user-secrets`:

```
dotnet user-secrets set "AzureAISettings:EndpointGPT4o" "<API endpoint>"
dotnet user-secrets set "AzureAISettings:EndpointLlama3_8b" "<API endpoint>"
dotnet user-secrets set "AzureAISettings:EndpointMinistral_3b" "<API endpoint>"
dotnet user-secrets set "AzureAISettings:ApiKeyGPT4o" "<API key>"
dotnet user-secrets set "AzureAISettings:ApiKeyLlama3_8b" "<API key>"
dotnet user-secrets set "AzureAISettings:ApiKeyMinistral_3b" "<API key>"
dotnet user-secrets set "AzureAISettings:AIAgentConnectionString" "<Connection string>"
```

## Codespaces

Add GitHub Codespaces secrets using the names:

```
AzureAISettings__EndpointGPT4o
AzureAISettings__EndpointLlama3_8b
AzureAISettings__EndpointMinistral_3b
AzureAISettings__ApiKeyGPT4o
AzureAISettings__ApiKeyLlama3_8b
AzureAISettings__ApiKeyMinistral_3b
AzureAISettings__AIAgentConnectionString
```

When running the Codespace open the terminal and use the Azure CLI to login in: `az login`