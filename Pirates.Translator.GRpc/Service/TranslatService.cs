using Grpc.Core;
using Pirates.Translator.GRpc.Primitives.Protos;
using Pirates.Translator.Primitives.Enums;
using Pirates.Translator.Services;
using static Pirates.Translator.GRpc.Primitives.Protos.TranslatService;

namespace Pirates.Translator.GRpc.Service;

public class TranslatService : TranslatServiceBase
{
    private readonly ITranslatorService _translatorService;
    public TranslatService(ITranslatorService translatorService)
    {
        _translatorService = translatorService;
    }
    public override async Task<TranslatResponse> Translate(TranslateRequest request, ServerCallContext context)
    {
        TranslatResponse response = new()
        {
            Result = await _translatorService.Translate((Language)request.Source, (Language)request.Destination, request.Text)
        };
      
        return response;
    }
}
