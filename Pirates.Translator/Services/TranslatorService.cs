using Pirates.Translator.Primitives.Enums;
using Pirates.Translator.Primitives.Extensions;
using System.Net;
using System.Text;
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

        string? result = await GetGTranslateResult(source, destination, text);

        if (string.IsNullOrEmpty(result))
            return null;

        return await PerformResult(text, result);

    }

    private async ValueTask<string?> GetGTranslateResult(Language source, Language destination, string text)
    {
        try
        {
            var toLanguage = destination.GetCode();
            var fromLanguage = source.GetCode();
            var url = $"https://translate.googleapis.com/translate_a/single?client=gtx&sl={fromLanguage}&tl={toLanguage}&dt=t&q={HttpUtility.UrlEncode(text)}";
            var webClient = new WebClient
            {
                Encoding = Encoding.UTF8
            };

            return webClient.DownloadString(url);
        }
        catch (Exception)
        {

            return null;
        }


    }
    private async ValueTask<string?> PerformResult(string originalText, string gTranslatedResult)
    {
        StringBuilder sb = new();
        try
        {
            string[]? translated = gTranslatedResult.Replace("'","{2315}").Replace("\"", "'").Split("['");
            foreach (var item in translated)
            {
                string[]? subItems = item.Split(",");
                if (subItems is null || subItems.Length < 2)
                    continue;

                if (originalText.Contains(subItems[1].Replace("'", "").Replace("{2315}","'")))
                    sb.Append(subItems.First().Replace("'", "").Replace("{2315}", "'"));
            }
            return sb.ToString();
        }
        catch
        {
            return null;
        }
    }
}
