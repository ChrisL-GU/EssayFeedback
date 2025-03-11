namespace EssayFeedback.Components.Pages;

using Common;
using Microsoft.AspNetCore.Components;
using Microsoft.FeatureManagement;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.Agents;
using Microsoft.SemanticKernel.Agents.Chat;
using Microsoft.SemanticKernel.ChatCompletion;
using MudBlazor;
using Strategies;

public partial class Home : ComponentBase
{
    private AgentGroupChat chat = new();
    
    [Inject]
    public required IDialogService DialogService { get; set; }
    [Inject]
    public required IFeatureManagerSnapshot FeatureManager { get; set; }
        
    private string EssayText { get; set; } = string.Empty;
    private MarkupString RenderedModelMarkdown { get; set; }
    private MarkupString RenderedRecommendationMarkdown { get; set; }

    protected override async Task OnInitializedAsync()
    {
        if (await FeatureManager.IsEnabledAsync("DefaultEssayText")) EssayText = TextToAnalyze.PaulGraham;
        Settings settings = new();
        var builder = Kernel.CreateBuilder();

        builder.AddAzureOpenAIChatCompletion(
            settings.AzureOpenAi.ChatModelDeployment,
            settings.AzureOpenAi.Endpoint,
            settings.AzureOpenAi.ApiKey);
        var kernel = builder.Build();

        var agents = new EssayAgents(kernel);

        chat =
            new AgentGroupChat(agents.GetAgents())
            {
                ExecutionSettings = new AgentGroupChatSettings
                {
                    SelectionStrategy = new SequentialSelectionStrategy(),
                    TerminationStrategy = new ApprovalTerminationStrategy()
                    {
                        Agents = [agents.FinalAgent],
                        MaximumIterations = agents.GetAgents().Length // * 3,
                    }
                }
            };
    }

    private async Task AnalyzeText()
    {
        var text = string.Empty;
        var parameters = new DialogParameters
        {
            [nameof(ModelProcessingDialog.TextToAnalyze)] = text
        };
        var dialog = await DialogService.ShowAsync<ModelProcessingDialog>(
            "Analyzing Text",
            parameters,
            new DialogOptions {MaxWidth = MaxWidth.Medium, FullWidth = true,  Position = DialogPosition.TopCenter} );
        var dialogInstance = dialog.Dialog as ModelProcessingDialog;
        
        chat.AddChatMessage(new ChatMessageContent(AuthorRole.User, EssayText));
        chat.IsComplete = false;
        
        await foreach (var chatMessageContent in chat.InvokeStreamingAsync())
        {
            text += chatMessageContent.Content ?? "";
            dialogInstance?.UpdateText(text);
        }
            
        var history = await chat.GetChatMessagesAsync().ToArrayAsync();
        
        var modelAnalysis = history[0].Content?.GetBetween("<essay_analysis>", "</essay_analysis>") +
            history[1].Content?.GetBetween("<essay_analysis>", "</essay_analysis>") +
            history[2].Content?.GetBetween("<essay_analysis>", "</essay_analysis>") ?? ""
            .Trim();
        var modelFeedback = history[0].Content?.GetBetween("<feedback>", "</feedback>") +
            history[1].Content?.GetBetween("<feedback>", "</feedback>") +
            history[2].Content?.GetBetween("<feedback>", "</feedback>")
            .Trim();
        
        RenderedRecommendationMarkdown = (MarkupString)Markdig.Markdown.ToHtml(modelFeedback);
        RenderedModelMarkdown = (MarkupString)Markdig.Markdown.ToHtml(modelAnalysis);
        
        DialogService.Close(dialog);
    
        // ShowSkeleton = true;
        // ShowAnswer = false;
        // var queryResponse = await searchService.Search(new SearchData(Topic, Duration, LessonComponents.ToList()));
        // QueryResults.Clear();
        // QueryResults.Add(queryResponse);
        // ShowSkeleton = false;
        // ShowAnswer = true;
    }

}