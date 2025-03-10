namespace EssayFeedback.Components.Pages;

using Microsoft.AspNetCore.Components;
using MudBlazor;

public partial class Home : ComponentBase
{
    public record ChatResponse(string Author, string Reasoning, List<Feedback> Feedbacks);
    public record Feedback(string OriginalText, string Recommendation, string Explanation);
    public record FeedbackResponse(List<Feedback> Feedback);

    protected override void OnInitialized()
    {
        RenderedRecommendationMarkdown = (MarkupString)Markdig.Markdown.ToHtml(recommendationMarkdown);
        RenderedModelMarkdown = (MarkupString)Markdig.Markdown.ToHtml(modelMarkdown);
    }
    
    private async Task AnalyzeText()
    {
        await DialogService.ShowAsync<ModelProcessingDialog>(
            "Custom Options Dialog",
            new DialogOptions {MaxWidth = MaxWidth.Medium, FullWidth = true, Position = DialogPosition.TopCenter} );
        // ShowSkeleton = true;
        // ShowAnswer = false;
        // var queryResponse = await searchService.Search(new SearchData(Topic, Duration, LessonComponents.ToList()));
        // QueryResults.Clear();
        // QueryResults.Add(queryResponse);
        // ShowSkeleton = false;
        // ShowAnswer = true;
    }

    private MarkupString EssayTextConverted { get; set; }
    private string essayText = """
                                           I'm usually reluctant to make predictions about technology, but I feel fairly confident about this one: in a couple decades there won't be many people who can write.

                                           One of the strangest things you learn if you're a writer is how many people have trouble writing. Doctors know how many people have a mole they're worried about; people who are good at setting up computers know how many people aren't; writers know how many people need help writing.

                                           The reason so many people have trouble writing is that it's fundamentally difficult. To write well you have to think clearly, and thinking clearly is hard.

                                           And yet writing pervades many jobs, and the more prestigious the job, the more writing it tends to require.

                                           These two powerful opposing forces, the pervasive expectation of writing and the irreducible difficulty of doing it, create enormous pressure. This is why eminent professors often turn out to have resorted to plagiarism. The most striking thing to me about these cases is the pettiness of the thefts. The stuff they steal is usually the most mundane boilerplate — the sort of thing that anyone who was even halfway decent at writing could turn out with no effort at all. Which means they're not even halfway decent at writing.

                                           Till recently there was no convenient escape valve for the pressure created by these opposing forces. You could pay someone to write for you, like JFK, or plagiarize, like MLK, but if you couldn't buy or steal words, you had to write them yourself. And as a result nearly everyone who was expected to write had to learn how.

                                           Not anymore. AI has blown this world open. Almost all pressure to write has dissipated. You can have AI do it for you, both in school and at work.

                                           The result will be a world divided into writes and write-nots. There will still be some people who can write. Some of us like it. But the middle ground between those who are good at writing and those who can't write at all will disappear. Instead of good writers, ok writers, and people who can't write, there will just be good writers and people who can't write.

                                           Is that so bad? Isn't it common for skills to disappear when technology makes them obsolete? There aren't many blacksmiths left, and it doesn't seem to be a problem.

                                           Yes, it's bad. The reason is something I mentioned earlier: writing is thinking. In fact there's a kind of thinking that can only be done by writing. You can't make this point better than Leslie Lamport did:

                                           If you're thinking without writing, you only think you're thinking.

                                           So a world divided into writes and write-nots is more dangerous than it sounds. It will be a world of thinks and think-nots. I know which half I want to be in, and I bet you do too.

                                           This situation is not unprecedented. In preindustrial times most people's jobs made them strong. Now if you want to be strong, you work out. So there are still strong people, but only those who choose to be.

                                           It will be the same with writing. There will still be smart people, but only those who choose to be.
                                           """;

    private MarkupString RenderedModelMarkdown { get; set; }
    private string modelMarkdown = """
    ### Sentence-by-Sentence Analysis for AP Stylebook Violations

    1. **Sentence**: "I'm usually reluctant to make predictions about technology, but I feel fairly confident about this one: in a couple decades there won't be many people who can write."
       - **Potential Violation**: "in a couple decades"
       - **AP Style Rule**: Use figures for all numbers 10 and above; spell out numbers below 10 unless part of an address or other technical forms.
       - **Analysis**: The phrase "a couple decades" should specify numerically if it's exact (e.g., "two decades"). However, expressing vague time as "a couple" may not necessarily violate AP Style rules. The stylistic phrasing is acceptable in informal contexts.

       No violations present.

    2. **Sentence**: "One of the strangest things you learn if you're a writer is how many people have trouble writing."
       - No AP Style violations present.

    3. **Sentence**: "Doctors know how many people have a mole they're worried about; people who are good at setting up computers know how many people aren't; writers know how many people need help writing."
       - **Potential Violation**: Serial semicolons.
       - **AP Style Rule**: Use semicolons to separate elements in a list if they have internal commas. Otherwise, use commas.
       - **Analysis**: The semicolons in this context are used correctly to separate independent clauses, and thus no violation occurs.

       No violations present.

    4. **Sentence**: "The reason so many people have trouble writing is that it's fundamentally difficult."
       - No AP Style violations present.

    5. **Sentence**: "To write well you have to think clearly, and thinking clearly is hard."
       - No AP Style violations present.

    6. **Sentence**: "And yet writing pervades many jobs, and the more prestigious the job, the more writing it tends to require."
       - **Potential Violation**: Starting a sentence with "And."
       - **AP Style Rule**: It is acceptable to start a sentence with "and" for effect or flow in informal, narrative contexts.
       - **Analysis**: No violation here as "and" is used editorially.

       No violations present.

    7. **Sentence**: "These two powerful opposing forces, the pervasive expectation of writing and the irreducible difficulty of doing it, create enormous pressure."
       - No AP Style violations present.

    8. **Sentence**: "This is why eminent professors often turn out to have resorted to plagiarism."
       - No AP Style violations present.

    9. **Sentence**: "The most striking thing to me about these cases is the pettiness of the thefts."
       - No AP Style violations present.

    10. **Sentence**: "The stuff they steal is usually the most mundane boilerplate — the sort of thing that anyone who was even halfway decent at writing could turn out with no effort at all."
        - **Potential Violation**: Em dash use.
        - **AP Style Rule**: Use a single hyphen in lieu of an em dash where en dashes aren't available.
        - **Analysis**: AP Style prefers proper formatting for the em dash. The typical print format or online editor would omit spaces around the dash. Depending on formatting tools, this might be condensed into a single symbol.

        No violations present since many em dash interpretations respect default appliance.

    11. **Sentence**: "Which means they're not even halfway decent at writing."
        - **Potential Violation**: Sentence fragment.
        - **AP Style Rule**: Sentence fragments are acceptable in informal writing depending on the tone.
        - **Analysis**: No strict violation of AP Style considering that the casual tone supports the fragment for emphasis.

        No violations present.

    12. **Sentence**: "Till recently there was no convenient escape valve for the pressure created by these opposing forces."
        - **Potential Violation**: "Till recently."
        - **AP Style Rule**: Avoid using "till"; prefer "until" in most cases, as per AP guidelines.
        - **Analysis**: "Till" is outdated usage and violates AP Style, which prefers "until."

        **Violation Identified**: Use of "Till" instead of "until."

    13. **Sentence**: "You could pay someone to write for you, like JFK, or plagiarize, like MLK, but if you couldn't buy or steal words, you had to write them yourself."
        - **Potential Violation**: Abbreviation of notable figures.
        - **AP Style Rule**: Full names of individuals should be used on first reference unless the initials are widely recognized.
        - **Analysis**: While "JFK" and "MLK" are widely known, AP Style prefers first and last names spelled out on first reference.

        **Violation Identified**: Omission of full names in first references to "JFK" and "MLK."

    14. **Sentence**: "And as a result nearly everyone who was expected to write had to learn how."
        - No AP Style violations present.

    15. **Sentence**: "Not anymore."
        - **Potential Violation**: Sentence fragment.
        - **AP Style Rule**: Sentence fragments are accepted for emphasis or literary style.
        - **Analysis**: No strict rule is broken here due to tone.

        No violations present.

    16. **Sentence**: "AI has blown this world open."
        - No AP Style violations present.

    17. **Sentence**: "Almost all pressure to write has dissipated."
        - No AP Style violations present.

    18. **Sentence**: "You can have AI do it for you, both in school and at work."
        - No AP Style violations present.

    19. **Sentence**: "The result will be a world divided into writes and write-nots."
        - No AP Style violations present.

    20. **Sentence**: "There will still be some people who can write."
        - No AP Style violations present.

    21. **Sentence**: "Some of us like it."
        - No AP Style violations present.

    22. **Sentence**: "But the middle ground between those who are good at writing and those who can't write at all will disappear."
        - No AP Style violations present.

    23. **Sentence**: "Instead of good writers, ok writers, and people who can't write, there will just be good writers and people who can't write."
        - **Potential Violation**: Use of "ok."
        - **AP Style Rule**: Always spell out "OK" in uppercase letters. Do not use lowercase "ok."
        - **Analysis**: "OK" written as "ok" is a violation of AP Style rules because the guide explicitly states the correct form.

        **Violation Identified**: Incorrect use of "ok" instead of "OK."

    24. **Sentence**: "Is that so bad?"
        - No AP Style violations present.

    25. **Sentence**: "Isn't it common for skills to disappear when technology makes them obsolete?"
        - No AP Style violations present.

    26. **Sentence**: "There aren't many blacksmiths left, and it doesn't seem to be a problem."
        - No AP Style violations present.

    27. **Sentence**: "Yes, it's bad."
        - No AP Style violations present.

    28. **Sentence**: "The reason is something I mentioned earlier: writing is thinking."
        - No AP Style violations present.

    29. **Sentence**: "In fact there's a kind of thinking that can only be done by writing."
        - No AP Style violations present.

    30. **Sentence**: "You can't make this point better than Leslie Lamport did:"
        - No AP Style violations present.

    31. **Sentence**: "If you're thinking without writing, you only think you're thinking."
        - No AP Style violations present.

    32. **Sentence**: "So a world divided into writes and write-nots is more dangerous than it sounds."
        - No AP Style violations present.

    33. **Sentence**: "It will be a world of thinks and think-nots."
        - No AP Style violations present.

    34. **Sentence**: "I know which half I want to be in, and I bet you do too."
        - No AP Style violations present.

    35. **Sentence**: "This situation is not unprecedented."
        - No AP Style violations present.

    36. **Sentence**: "In preindustrial times most people's jobs made them strong."
        - No AP Style violations present.

    37. **Sentence**: "Now if you want to be strong, you work out."
        - No AP Style violations present.

    38. **Sentence**: "So there are still strong people, but only those who choose to be."
        - No AP Style violations present.

    39. **Sentence**: "It will be the same with writing."
        - No AP Style violations present.

    40. **Sentence**: "There will still be smart people, but only those who choose to be."
        - No AP Style violations present.

    ### Summary of Findings and Violations
    - Violation related to "till" vs. "until."
    - Violation related to first reference of "JFK" and "MLK."
    - Violation related to incorrect formatting of "ok" instead of "OK."

                                                        
    ### Analysis of Essay Tone Consistency

    #### Intended Overall Tone:
    - The essay adopts an **informative and reflective tone** with moments of conversational and persuasive elements. The author uses a first-person perspective to engage readers while making observations about society, technology, and human cognition.

    ---

    ### Paragraph Analysis

    #### Paragraph 1:
    1. **Exemplary Phrases/Sentences**:
       - "I'm usually reluctant to make predictions about technology..."
       - "...in a couple decades there won't be many people who can write."
    2. **Tone Elements**: 
       - Conversational and personal tone ("I'm usually reluctant" and "I feel fairly confident").
       - Direct and slightly provocative by suggesting a bold prediction.
    3. **Tone Analysis**:
       - The tone is engaging and confident, establishing personal authority. However, the conversational style may not fully align with the essay's later reflective tone. 

    #### Paragraph 2:
    1. **Exemplary Phrases/Sentences**:
       - "One of the strangest things you learn if you're a writer..."
       - "...how many people need help writing."
    2. **Tone Elements**:
       - Informal language with contractions ("you're" and "how many people").
       - Empathetic tone and grounded in personal observation.
    3. **Tone Analysis**:
       - The conversational tone continues here, with effective use of relatable examples. No significant tone shifts.

    #### Paragraph 3:
    1. **Exemplary Phrases/Sentences**:
       - "The reason so many people have trouble writing is that it's fundamentally difficult."
       - "To write well you have to think clearly, and thinking clearly is hard."
    2. **Tone Elements**:
       - Direct and mildly formal language.
       - Repetition emphasizes the reflective nature of the text.
    3. **Tone Analysis**:
       - Consistently reflective and analytical. Matches the subject's depth.

    #### Paragraphs 4-5:
    1. **Exemplary Phrases/Sentences**:
       - "...the more prestigious the job, the more writing it tends to require."
       - "...create enormous pressure."
    2. **Tone Elements**:
       - Analytical verbs and formal word choice ("pervades," "prestigious," "enormous pressure").
       - Acknowledges broader societal trends in a neutral tone.
    3. **Tone Analysis**:
       - The formality increases here, making the tone slightly more serious than in the earlier paragraphs. However, the shift is appropriate for the argument's progression.

    #### Paragraphs 6-9:
    1. **Exemplary Phrases/Sentences**:
       - "This is why eminent professors often turn out to have resorted to plagiarism."
       - "...the sort of thing that anyone who was even halfway decent at writing could turn out with no effort at all."
    2. **Tone Elements**:
       - Condescending and judgmental phrasing ("not even halfway decent at writing").
       - Informal conversational markers remain ("the sort of thing").
    3. **Tone Analysis**:
       - The tone briefly becomes more critical and less neutral, which could be perceived as dismissive. While it adds emphasis, the informal word choice ("sort of thing") detracts from the argument's authority.

    #### Paragraphs 10-15:
    1. **Exemplary Phrases/Sentences**:
       - "Till recently there was no convenient escape valve for the pressure created by these opposing forces."
       - "Not anymore."
    2. **Tone Elements**:
       - Short, impactful sentences and informal language.
       - A conversational tone with rhetorical emphasis.
    3. **Tone Analysis**:
       - The paragraph effectively builds tension, though the contraction "till" instead of "until" disrupts the otherwise serious tone.

    #### Paragraph 16-19:
    1. **Exemplary Phrases/Sentences**:
       - "AI has blown this world open."
       - "...a world divided into writes and write-nots."
    2. **Tone Elements**:
       - Use of metaphor ("blown this world open") creates vivid imagery.
       - Casual tone continues ("write-nots").
    3. **Tone Analysis**:
       - While the conversational tone is engaging, some rhetoric, like "write-nots," may feel overly informal or whimsical compared to earlier paragraphs.

    #### Paragraphs 20-22:
    1. **Exemplary Phrases/Sentences**:
       - "Is that so bad?"
       - "...there will just be good writers and people who can't write."
    2. **Tone Elements**:
       - Rhetorical questions and informal comparisons.
       - Direct phrasing maintains accessibility.
    3. **Tone Analysis**:
       - Conversational tone becomes slightly provocative. It's effective in engaging readers but diverges from the formal analysis earlier.

    #### Paragraphs 23-End:
    1. **Exemplary Phrases/Sentences**:
       - "Writing is thinking."
       - "You can't make this point better than Leslie Lamport did."
    2. **Tone Elements**:
       - Simple, declarative sentences emphasize persuasion.
       - A mix of formal declarations and personal anecdotes.
    3. **Tone Analysis**:
       - The tone strategically blends authority ("Leslie Lamport") with relatability. However, broad statements like "writing is thinking" may feel oversimplified.

    ---

    ### Summary of Tone Consistency
    1. **Tone Shifts Identified**:
       - A gradual shift from conversational to reflective tones occurs in the middle paragraphs, with minor moments of critique or condescension (e.g., "not even halfway decent at writing").
       - The use of casual or whimsical elements (e.g., "write-nots") occasionally disrupts the essay's flow.

    2. **Appropriateness of Shifts**:
       - The shifts are largely appropriate since conversational tones engage readers early on, and a more reflective tone supports the essay's argument. However, some informal elements (e.g., "sort of thing," rhetorical phrases) weaken the authority of the main points.

    3. **Disciplinary Conventions**:
       - Essays discussing systemic societal changes and technological impacts often maintain a formal and reflective tone. Thus, the conversational language is suitable for audience engagement, but excessive informality (e.g., "ok writers," "write-nots") may detract from the credibility of the argument.

    4. **Strengths in Tone Management**:
       - The essay's mix of personal anecdotes with broader societal observations creates an effective narrative arc.
       - The reflective tone in later sections ("writing is thinking") aligns well with the author's thesis.

    5. **Weaknesses in Tone Management**:
       - Abrupt tone shifts (e.g., from conversational to judgmental) may feel inconsistent.
       - Informal phrases and whimsical metaphors ("write-nots") could diminish the essay's perceived depth.

    ---

    ### Key Claims in the Essay
    1. Writing is fundamentally difficult because it requires clear thinking, which many people struggle with.
    2. Writing is pervasive in prestigious jobs, creating significant pressure.
    3. Technology, particularly AI, is removing the necessity for most people to learn how to write.
    4. The disappearance of writing skills will result in a societal divide between people who can write (and think) and those who cannot.
    5. Writing is not just a skill but a core part of thinking, and its decline will lead to intellectual stagnation for those who choose not to engage with it.

    ---

    ### Bias Analysis

    #### Language
    - **Emotional Bias**: 
      - Quote: "This is why eminent professors often turn out to have resorted to plagiarism."
        - The use of "eminent professors" adds a judgmental tone, implying that even those who are highly regarded are morally flawed. This may provoke an emotional response by linking unethical behavior to societal expectations about writing.
      - Quote: "Instead of good writers, ok writers, and people who can't write, there will just be good writers and people who can't write."
        - The phrase "ok writers" and the binary "good writers and people who can't write" oversimplify the diversity in writing skills. It is emotionally charged to frame this shift as a stark and unnerving divide.
      - Quote: "It will be a world of thinks and think-nots."
        - The playful yet provocative language assumes a moralistic perspective that thinking without writing is inherently lesser, creating a sense of alarm.

    #### Framing
    - **Framing Bias**:
      - Quote: "AI has blown this world open. Almost all pressure to write has dissipated. You can have AI do it for you, both in school and at work."
        - The essay frames AI-driven changes as dangerous without addressing potential benefits or nuance (e.g., how AI might assist in teaching and improving writing). This one-sided framing positions AI solely as a threat to cognitive skills.
      - Quote: "The result will be a world divided into writes and write-nots."
        - The metaphor of "writes and write-nots" simplifies a complex topic into a binary division, neglecting potential middle grounds where individuals integrate writing and AI collaboratively.

    #### Reasoning
    - **Confirmation Bias**:
      - The essay repeatedly emphasizes that writing is in decline due to AI but does not provide evidence or data to substantiate its claims. It assumes the technological shift will inherently lead to widespread skill erosion without considering historical precedent or adaptability.
      - Failure to cite examples of individuals or institutions leveraging AI positively in writing (e.g., as an educational tool) reflects selective reasoning, favoring evidence that supports the argument.
    - **Oversimplification**:
      - The assertion that "writing is thinking" and the prediction of a sharp divide between thinkers and non-thinkers reduce a complex relationship between writing, cognition, and technology into an all-or-nothing scenario.

    ---

    ### Identification of Specific Biases

    #### Confirmation Bias
    - **Evidence**:
      - "Writing pervades many jobs, and the more prestigious the job, the more writing it tends to require."
      - While this claim may hold true for certain professions, it overlooks roles that rely less heavily on writing and more on interpersonal or technical skills. It assumes a one-size-fits-all relationship without exploring counterexamples.
    - **Impact**:
      - This reasoning reinforces the author's argument without exploring alternative perspectives, possibly skewing reader perception to believe that writing is universally indispensable.

    #### Selection Bias
    - **Evidence**:
      - "You could pay someone to write for you, like JFK, or plagiarize, like MLK."
      - The historical references to John F. Kennedy and Martin Luther King Jr. omit broader social contexts and only highlight examples that align with the argument. It disregards cases of individuals excelling through collaborative writing or mentorship.
    - **Impact**:
      - These cherry-picked examples lend undue weight to the argument that reliance on external help indicates a decline in writing ability.

    #### Framing Bias
    - **Evidence**:
      - "AI has blown this world open."
      - The language positions AI as a disruptive and negative force without acknowledging how it might enhance human collaboration, creativity, or literacy.
    - **Impact**:
      - This framing primes readers to view AI as harmful, precluding more balanced interpretations of its role in writing.

    #### Emotional Bias
    - **Evidence**:
      - "The result will be a world divided into writes and write-nots."
      - The stark dichotomy and ominous tone evoke fear and urgency, potentially swaying readers emotionally rather than rationally.
    - **Impact**:
      - The emotional framing reduces the complexity of the argument, encouraging readers to react with concern rather than analyze the nuances of the issue.

    #### Oversimplification
    - **Evidence**:
      - "A world divided into writes and write-nots is more dangerous than it sounds."
      - The claim ignores potential adaptive responses, such as the integration of AI tools to complement human writing skills.
    - **Impact**:
      - The oversimplified argument may lead readers to draw black-and-white conclusions about the future of writing and thinking.

    ---

    ### Thought Process and Observations
    - **Language**: The author frequently uses emotionally charged language to emphasize the perceived dangers of declining writing skills due to AI. While effective in engaging readers, it subtly biases the argument by casting AI in a threatening light.
    - **Framing**: The essay frames the decline of writing as inevitable and detrimental without considering positive or neutral outcomes. This framing could mislead readers by presenting an incomplete picture.
    - **Reasoning**: Logical fallacies, such as confirmation bias and oversimplification, weaken the essay's argument. By selectively presenting evidence and ignoring counterarguments, the author reduces the complexity of the writing-AI relationship.

    ---

    ### Counterarguments
    1. **Claim**: "Writing is thinking."
       - **Counterargument**: Many forms of thinking, such as visual or oral communication, do not require writing. Technologies like AI can enhance rather than detract from these alternative cognitive processes.
    2. **Claim**: "AI has blown this world open. Almost all pressure to write has dissipated."
       - **Counterargument**: AI can serve as a tool to assist and amplify writing efforts, enabling people to focus on higher-level thinking rather than eliminating the skill altogether.
    3. **Claim**: "The result will be a world divided into writes and write-nots."
       - **Counterargument**: The binary framing disregards scenarios where AI and writing coexist, allowing individuals to acquire hybrid skills.

    ---

    ### Strengths and Weaknesses of the Essay
    - **Strengths**:
      - The essay raises valid concerns about the impact of technology on fundamental skills like writing and thinking.
      - The conversational tone engages readers and makes complex ideas accessible.
    - **Weaknesses**:
      - The heavy reliance on emotional appeals and selective evidence weakens the argument's credibility.
      - The lack of nuance and alternative perspectives reduces the essay's depth and persuasiveness.
    """;

    private MarkupString RenderedRecommendationMarkdown { get; set; }
    private string recommendationMarkdown = """
    # AP Style Violations

    1. **Original Text**: "The stuff they steal is usually the most mundane boilerplate - the sort of thing that anyone who was even halfway decent at writing could turn out with no effort at all."
       - **AP Stylebook Rule**: AP Style requires em dashes to have spaces on both sides.
       - **Explanation**: The em dash lacks spaces on either side, violating AP Style formatting guidelines.

    2. **Original Text**: "Till recently there was no convenient escape valve for the pressure created by these opposing forces."
       - **AP Stylebook Rule**: Use "until" rather than the informal "till."
       - **Explanation**: "Till" is considered informal and not standard AP Style usage.

    3. **Original Text**: "Instead of good writers, ok writers, and people who can't write, there will just be good writers and people who can't write."
       - **AP Stylebook Rule**: AP Style requires "OK" to be written in full capital letters.
       - **Explanation**: The lowercase "ok" does not align with AP Style's proper capitalization.

    4. **Original Text**: "You can't make this point better than Leslie Lamport did:"
       - **AP Stylebook Rule**: Quotations should be formatted appropriately, and block quotes are discouraged.
       - **Explanation**: The colon at the end of the sentence creates ambiguity about the formatting of the upcoming quotation, which might not align with AP Style conventions.
       
    # Tone Consistency Evaluation

    1. **Replace informal word choices with more formal alternatives.**
       - Example: Replace "Till recently" with "Until recently" in Paragraph 6.
       - Explanation: "Till" feels overly casual and inconsistent with the reflective tone. This change adds polish and appropriateness.

    2. **Standardize usage of abbreviations and capitalizations to enhance credibility.**
       - Example: Change "ok writers" to "OK writers" in Paragraph 8.
       - Explanation: The lowercase "ok" appears informal and does not align with the essay's polished point. Using "OK" demonstrates attention to detail.

    3. **Consider reviewing contractions in formal sections.**
       - Example: "It's fundamentally difficult" could be rewritten as "It is fundamentally difficult" in Paragraph 3.
       - Explanation: Reducing contractions in key analytical sections creates a more serious and reflective tone, suitable for persuasive writing.

    4. **Reassess the use of playful phrasing in serious contexts.**
       - Example: "Writes and write-nots" in Paragraph 8.
       - Explanation: While engaging, this phrase may risk trivializing the argument, particularly for a formal audience. Simplifying the phrase would enhance clarity.

    5. **Balance abrupt declarative statements for tone smoothness.**
       - Example: "Yes, it's bad" in Paragraph 9 could be expanded to: "Yes, this situation is problematic."
       - Explanation: Abrupt, overly casual statements can feel jarring and less impactful. Elaborating slightly maintains engagement without sacrificing the reflective tone.

    # Identified Biases

    1. Framing Bias
    - **Original Text**: "AI has blown this world open."
    - **Explanation**: This emotionally charged phrasing portrays AI as overwhelmingly disruptive, potentially skewing the reader's perception of AI as a negative force.

    2. In-group Bias
    - **Original Text**: "Some of us like it."
    - **Explanation**: This language subtly aligns the author with "writers" as an in-group, creating an implicit division and favoring one group over another.

    3. Stereotyping
    - **Original Text**: "Eminent professors often turn out to have resorted to plagiarism."
    - **Explanation**: This broad statement unfairly generalizes professors, implying a systemic tendency to plagiarize without offering evidence or context.

    4. Selection Bias
    - **Original Text**: The essay focuses entirely on the negative implications of AI replacing writing skills.
    - **Explanation**: By ignoring potential benefits of AI, such as accessibility and enhanced creativity, the essay presents a one-sided argument.

    5. False Equivalence
    - **Original Text**: "There aren't many blacksmiths left, and it doesn't seem to be a problem."
    - **Explanation**: Comparing writing-a fundamental skill-to blacksmithing oversimplifies the issue and minimizes the broader implications of losing writing abilities.

    6. Oversimplification
    - **Original Text**: "Instead of good writers, ok writers, and people who can't write, there will just be good writers and people who can't write."
    - **Explanation**: This assertion oversimplifies the anticipated impact of AI on writing skills, ignoring its role in skill enhancement or education.

    7. Confirmation Bias
    - **Original Text**: "The most striking thing to me about these cases is the pettiness of the thefts... Which means they're not even halfway decent at writing."
    - **Explanation**: The author assumes plagiarism is exclusively due to incompetence, confirming their narrative that writing is a challenging skill.

    8. Authority Bias
    - **Original Text**: "If you're thinking without writing, you only think you're thinking."
    - **Explanation**: The essay heavily relies on this quote without critically examining its applicability, potentially overemphasizing a single authoritative opinion.

    """;
}