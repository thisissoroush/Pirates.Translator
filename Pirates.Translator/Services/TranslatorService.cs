using Pirates.Translator.Primitives.Enums;
using Pirates.Translator.Primitives.Extensions;
using System.Net;
using System.Web;

namespace Pirates.Translator.Services;

public interface ITranslatorService
{
    ValueTask<string> Translate(Language source, Language destination, string text);
}
public class TranslatorService : ITranslatorService
{
    public async ValueTask<string> Translate(Language source, Language destination, string text)
    {
        if (source.Equals(destination) || string.IsNullOrEmpty(text))
            return text;


        var toLanguage = destination.GetCode();
        var fromLanguage = source.GetCode();
        var url = $"https://translate.googleapis.com/translate_a/single?client=gtx&sl={fromLanguage}&tl={toLanguage}&dt=t&q={HttpUtility.UrlEncode(text)}";
        var webClient = new WebClient
        {
            Encoding = System.Text.Encoding.UTF8
        };
        var result = webClient.DownloadString(url);
        try
        {
            result = result.Substring(4, result.IndexOf("\"", 4, StringComparison.Ordinal) - 4);
            return result;
        }
        catch
        {
            return null;
        }
    }

}
