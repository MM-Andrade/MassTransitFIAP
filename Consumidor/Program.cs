using Consumidor;
using Consumidor.Eventos;
using MassTransit;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddHostedService<Worker>();

var configuration = builder.Configuration;
var fila = configuration.GetSection("MassTransit")["NomeFila"] ?? string.Empty;
var servidor = configuration.GetSection("MassTransit")["Servidor"] ?? string.Empty;
var usuario = configuration.GetSection("MassTransit")["Usuario"] ?? string.Empty;
var senha = configuration.GetSection("MassTransit")["Senha"] ?? string.Empty;

builder.Services.AddMassTransit(x =>
{
    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host(servidor, "/", h =>
        {
            h.Username(usuario);
            h.Password(senha);
        });

        cfg.ReceiveEndpoint(fila, e =>
        {
            e.Consumer<PedidoCriadoConsumidor>();
        });

        cfg.ConfigureEndpoints(context);
    });

    x.AddConsumer<PedidoCriadoConsumidor>();
});

var host = builder.Build();
host.Run();
