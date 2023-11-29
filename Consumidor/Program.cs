using Consumidor;
using Consumidor.Eventos;
using MassTransit;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddHostedService<Worker>();

var configuration = builder.Configuration;
var fila = configuration.GetSection("MassTransitAzure")["NomeFila"] ?? string.Empty;
var topico = configuration.GetSection("MassTransitAzure")["NomeTopico"] ?? string.Empty;
var conexao = configuration.GetSection("MassTransitAzure")["Conexao"] ?? string.Empty;


builder.Services.AddMassTransit(x =>
{
    x.UsingAzureServiceBus((context, cfg) =>
    {
        cfg.Host(conexao);

       cfg.ReceiveEndpoint(fila, e =>
       {
           e.Consumer<PedidoCriadoConsumidor>();
           //e.PrefetchCount = 16;

       });

    });
});

var host = builder.Build();
host.Run();
