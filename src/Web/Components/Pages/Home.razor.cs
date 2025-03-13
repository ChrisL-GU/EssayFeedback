namespace EssayFeedback.Components.Pages;

using Common;
using Microsoft.AspNetCore.Components;
using Microsoft.FeatureManagement;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.Agents;
using Microsoft.SemanticKernel.ChatCompletion;
using MudBlazor;

public partial class Home : ComponentBase
{
    private AgentGroupChat chat = new();
    private string[] agentNames = [];
    private IEnumerable<string> selectedAgentNames = [];
    private string[] modelNames = [];
    private string selectedModel = "";

    [Inject]
    public required Settings Settings { get; set; }
    [Inject]
    public required AgentManagement AgentManagement { get; set; }
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
        modelNames = Settings.AzureAiSettings.Keys.ToArray();
        selectedModel = modelNames.First();

        agentNames = EssayAgents.GetAgentNames();
        selectedAgentNames = [agentNames.Last()];
    }

    private async Task AnalyzeText()
    {
        RenderedRecommendationMarkdown = new MarkupString();
        RenderedModelMarkdown = new MarkupString();
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

        chat = AgentManagement.CreateAgentGroupChatFor(selectedModel, selectedAgentNames.ToArray());
        chat.AddChatMessage(new ChatMessageContent(AuthorRole.User, EssayText));
        chat.IsComplete = false;
        
        await foreach (var chatMessageContent in chat.InvokeStreamingAsync())
        {
            text += chatMessageContent.Content ?? "";
            dialogInstance?.UpdateText(text);
        }

        var (modelAnalysis, modelFeedback) = await AgentManagement.AggergateAgentHistoryResponses(chat);
        RenderedRecommendationMarkdown = (MarkupString)Markdig.Markdown.ToHtml(modelFeedback);
        RenderedModelMarkdown = (MarkupString)Markdig.Markdown.ToHtml(modelAnalysis);
        
        DialogService.Close(dialog);
        dialogInstance?.UpdateText("");
    }

}