using Microsoft.Extensions.DependencyInjection;
using Pirates.Translator.Service.Primitives.Enums;
using Pirates.Translator.Service.Services;

namespace Pirates.Translator.Service.Initialization;

public static class Init
{
    public static void RegisterTranslator(this IServiceCollection services, OS osMode = OS.Windows, string? pythonPath = null)
    {
        services.AddScoped<ITranslatorService,TranslatorService>();
    }
}
