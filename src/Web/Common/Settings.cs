namespace EssayFeedback.Common;

using System.Reflection;

public class Settings
{
    private readonly IConfigurationRoot configRoot;

    private AzureAiSettings? azureAiSettings;

    public Settings()
    {
        configRoot =
            new ConfigurationBuilder()
                .AddEnvironmentVariables()
                .AddUserSecrets(Assembly.GetExecutingAssembly(), true)
                .Build();
    }

    //public Dictionary<string, ModelSettings> AzureAiSettings => azureAiSettings ??= new()
    
    public AzureAiSettings AzureAiSettings => azureAiSettings ??= new(
        new Dictionary<string, ModelSettings>
        {
            {
                "GPT-4o", new ModelSettings(
                    GetRequiredConfigValue("AzureAISettings:EndpointGPT4o"),
                    GetRequiredConfigValue("AzureAISettings:ApiKeyGPT4o"),
                    IsAzureOpenAiModel: true)

            },
            {
                "Meta-Llama-3-8B-Instruct", new ModelSettings(
                    GetRequiredConfigValue("AzureAISettings:EndpointLlama3-8b"),
                    GetRequiredConfigValue("AzureAISettings:ApiKeyLlama3-8b"))

            },
            {
                "Ministral-3b", new ModelSettings(
                    GetRequiredConfigValue("AzureAISettings:EndpointMinistral-3b"),
                    GetRequiredConfigValue("AzureAISettings:ApiKeyMinistral-3b"))
            }
        },
        new AgentSettings(GetRequiredConfigValue("AzureAISettings:AIAgentConnectionString")));

    private string GetRequiredConfigValue(string key) =>
        configRoot.GetValue<string>(key) ?? throw new NullReferenceException();
}

public record ModelSettings(string Endpoint, string ApiKey, bool IsAzureOpenAiModel = false);

public record AgentSettings(string ConnectionString);

public record AzureAiSettings(
    Dictionary<string, ModelSettings> ModelSettings,
    AgentSettings AgentSettings);