using Core;
using MassTransit;

namespace Consumidor.Eventos
{
    public class PedidoCriadoConsumidor : IConsumer<Pedido>
    {
        public Task Consume(ConsumeContext<Pedido> context)
        {

            //ao retornoar o objeto, ele já traz o objeto com o .tostring() por padrão
            Console.WriteLine(context.Message);

            return Task.CompletedTask;
        }
    }
}
