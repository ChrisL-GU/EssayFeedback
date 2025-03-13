namespace EssayFeedback.Common;

using System.Reflection;

public class Settings
{
    private readonly IConfigurationRoot configRoot;

    private Dictionary<string, AiSettings>? azureAiSettings;

    public Settings()
    {
        configRoot =
            new ConfigurationBuilder()
                .AddEnvironmentVariables()
                .AddUserSecrets(Assembly.GetExecutingAssembly(), true)
                .Build();
    }

    public Dictionary<string, AiSettings> AzureAiSettings => azureAiSettings ??= new()
    {
        {
            "GPT-4o", new AiSettings(
                GetRequiredConfigValue("AzureAISettings:EndpointGPT4o"),
                GetRequiredConfigValue("AzureAISettings:ApiKeyGPT4o"),
                IsAzureOpenAiModel: true)
            
        },
        {
            "Meta-Llama-3-8B-Instruct", new AiSettings(
                GetRequiredConfigValue("AzureAISettings:EndpointLlama3-8b"),
                GetRequiredConfigValue("AzureAISettings:ApiKeyLlama3-8b"))
            
        },
        {
            "Ministral-3b", new AiSettings(
                GetRequiredConfigValue("AzureAISettings:EndpointMinistral-3b"),
                GetRequiredConfigValue("AzureAISettings:ApiKeyMinistral-3b"))
        }
    };

    private string GetRequiredConfigValue(string key) =>
        configRoot.GetValue<string>(key) ?? throw new NullReferenceException();
}
public record AiSettings(string Endpoint, string ApiKey, bool IsAzureOpenAiModel = false);
