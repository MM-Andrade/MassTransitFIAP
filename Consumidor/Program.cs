using Consumidor;
using Consumidor.Eventos;
using MassTransit;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddHostedService<Worker>();

var configuration = builder.Configuration;
var fila = configuration.GetSection("MassTransitAzure")["NomeFila"] ?? string.Empty;
var topico = configuration.GetSection("MassTransitAzure")["Topico"] ?? string.Empty;
var conexao = configuration.GetSection("MassTransitAzure")["Conexao"] ?? string.Empty;


builder.Services.AddMassTransit(x =>
{
    x.UsingAzureServiceBus((context, cfg) =>
    {
        cfg.Host(conexao);

        cfg.SubscriptionEndpoint("sub-1", topico, e =>
        {
            e.Consumer<PedidoCriadoConsumidor>();
        });

    });
});

var host = builder.Build();
host.Run();
