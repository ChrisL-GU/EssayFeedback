## Introduction
This demo showcases an AI-powered application designed to provide feedback on essays. The application uses specialized AI agents, each focusing on a specific writing criterion.

## Usage

- Users enter their essay.
- Choose which writing criteria to analyze.
- Select the AI model to use for evaluation.

## Analysis

- As the essay is analyzed, a dialog displays the model's streaming responses.
- Two types of AI agents are used by the application.
    - The first type of agent relies solely on prompt engineering to analyze the essay. General guidelines are provided to the AI model on how to evaluate the essay but the agent uses its own knowledge to perform the evaluation.
    - The second type of agent uses both prompt engineering as well as incorporates knowledge articles to focus the evaluation and provide feedback.
- The agents operate together within a group chat, where each agent is allowed one turn in sequence. The group chat concludes after the last agent has responded.

## Feedback

- Once the analysis is complete, structured feedback appears to the right of the essay text for easy review and revisions of the text.
- Users can also access the raw AI-generated responses below the essay text for deeper insight on how the AI evaluated the text.
