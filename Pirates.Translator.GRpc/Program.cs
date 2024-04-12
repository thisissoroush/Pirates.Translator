using Pirates.Translator.GRpc.Service;
using Pirates.Translator.Initialization;

var builder = WebApplication.CreateBuilder(args);
builder.WebHost.ConfigureKestrel(serverOptions =>
{
    serverOptions.Limits.KeepAliveTimeout = TimeSpan.FromMinutes(2);
    serverOptions.Limits.MaxConcurrentConnections = 100;
    serverOptions.Limits.MaxConcurrentUpgradedConnections = 100;
    serverOptions.Limits.MaxRequestBodySize = 100_000_000;
});

builder.Services.AddGrpc();
builder.Services.RegisterTranslator();

var app = builder.Build();

app.UseHttpsRedirection();
app.UseRouting();
app.UseEndpoints(endpoints => {  
    endpoints.MapGrpcService<TranslatService>(); 

});


//app.MapGet("/", () => "Hello World!");

app.Run();
