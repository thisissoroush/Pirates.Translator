using System.Text.Json.Serialization;
using Pirates.Translator.Initialization;
using Pirates.Translator.Primitives.Enums;
using Pirates.Translator.Services;

var builder = WebApplication.CreateSlimBuilder(args);

builder.Services.ConfigureHttpJsonOptions(options =>
{
    options.SerializerOptions.TypeInfoResolverChain.Insert(0, AppJsonSerializerContext.Default);
});

builder.Services.RegisterTranslator();

var app = builder.Build();

var text = "Is this real? Or you might think it's not! Whatever, I'm going to test ( hello world! ).";
var translatorApi = app.MapGroup("/api");
translatorApi.MapGet("", async (ITranslatorService translator) => {
    var result = await translator.Translate(Language.English, Language.Persian, text);
    return string.IsNullOrEmpty(result) ? Results.NotFound() : Results.Ok(result);
    });
translatorApi.MapGet("/translate", async (Language source, Language destination, string text, ITranslatorService translator) =>
{
    var result = await translator.Translate(source, destination, text);
    return string.IsNullOrEmpty(result) ? Results.NotFound() : Results.Ok(result);
});


app.Run();


[JsonSerializable(typeof(string))]
internal partial class AppJsonSerializerContext : JsonSerializerContext
{

}
