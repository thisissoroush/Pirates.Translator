using Microsoft.Extensions.DependencyInjection;
using Pirates.Translator.Primitives.Enums;
using Pirates.Translator.Services;

namespace Pirates.Translator.Initialization;

public static class Init
{
    public static void RegisterTranslator(this IServiceCollection services, OS osMode = OS.Windows, string? pythonPath = null)
    {
        services.AddScoped<ITranslatorService,TranslatorService>();

    }
}
