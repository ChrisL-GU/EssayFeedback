namespace EssayFeedback.Common;

using System.Reflection;

public class Settings
{
    private readonly IConfigurationRoot configRoot;

    private AzureOpenAiSettings? azureOpenAi;

    public Settings()
    {
        configRoot =
            new ConfigurationBuilder()
                .AddEnvironmentVariables()
                .AddUserSecrets(Assembly.GetExecutingAssembly(), true)
                .Build();
    }

    public AzureOpenAiSettings AzureOpenAi => azureOpenAi ??= GetSettings<AzureOpenAiSettings>();

    public TSettings GetSettings<TSettings>()
    {
        return configRoot.GetRequiredSection(typeof(TSettings).Name).Get<TSettings>()!;
    }

    public class AzureOpenAiSettings
    {
        // ReSharper disable InconsistentNaming
        public string EndpointGpt4o { get; set; } = string.Empty;
        public string ApiKeyGpt4o { get; set; } = string.Empty;
        public string ApiKeyLlama { get; set; } = string.Empty;
        public string EndpointLlama { get; set; } = string.Empty;
        // ReSharper restore InconsistentNaming
    }
}