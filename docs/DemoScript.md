## Introduction
This demo showcases an AI-powered application designed to provide feedback on essays. The application uses specialized AI agents, each focusing on a specific writing criterion.  

## Usage

- Users enter their essay.
- Choose which writing criteria to analyze.
- Select the AI models to use for evaluation.

## Analysis

- As the essay is analyzed, a live dialog displays the model's streaming responses.
- Two types of AI agents are used by the application.
    - The first type of agent relies solely on prompt engineering to analyze the essay. General guidelines are provided to the AI model on how to evaluate the essay.
    - The second type of agent uses both prompt engineering and reference knowledge articles to provide deeper insights. 
- The agents operate in a structured group chat, where each agent is allowed one turn in sequence. The group chat concludes after the last agent has responded.

## Feedback

- Once the analysis is complete, structured feedback appears to the right of the essay for easy review and revisions of the essay text
- Users can also access the raw AI-generated responses below the essay for deeper insight.  
