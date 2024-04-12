
using Grpc.Net.Client;
using Pirates.Translator.GRpc.Primitives.Protos;
using Pirates.Translator.Primitives.Enums;
using System.Text;

var httpHandler = new HttpClientHandler();
httpHandler.ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;

using var grpcChannel = GrpcChannel.ForAddress("https://localhost:7106/", new GrpcChannelOptions { HttpHandler = httpHandler });

var client = new TranslatService.TranslatServiceClient(grpcChannel);

var targetlanguages = Enum.GetValues(typeof(Language)).Cast<Language>().ToList();

Console.OutputEncoding = Encoding.UTF8;

Parallel.ForEach(targetlanguages, language =>
{
    TranslateRequest request = new()
    {
        Source = (int)Language.English,
        Destination = (int)language,
        Text = "Welcome, This text is translated using grpc client!"
    };

    TranslatResponse response = client.Translate(request);
    Console.WriteLine($"{language.ToString()}: {response.Result}");
});



Console.ReadKey();
