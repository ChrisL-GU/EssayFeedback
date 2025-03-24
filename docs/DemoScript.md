## Describe

- This demo application utilizes AI models to provide feedback on essays.
- The application consists of AI agents, where each agent focuses on a specific writing criteria.

## Usage

- The user is able to enter their essay text.
- Select which criteria to analyze their essay.
- Select which models to use to perform the analysis.

## Analysis

- For demo purposes, while the text is being analyzed a dialog will be displayed streaming the content provided by the model while is analyzing the essay.
- Two types of agents are currently in use. One type of agent is defined entirely using prompt engineering to instruct the agent on how to analyze the essay.
- The second agent is defined in the cloud. In addition to prompt engineering it also makes use of an knowledge article on the topic of the evaluation which is uses to perform its analysis.
- The agents are configured to operate within group chat. Currently each agent is allowed one turn. They take their turn in sequence and the group chat is terminated once the last agent has responded.

## Feedback

- Once the analysis is complete the feedback provided by the model will be displayed to the right of the essay allowing the user to review the provided feedback and make any changes to their essay.
- Additionally, below the essay text the raw content response of the model can be revealed and reviewed.
