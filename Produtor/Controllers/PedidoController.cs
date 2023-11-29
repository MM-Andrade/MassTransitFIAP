using Core;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using System.Formats.Asn1;

namespace Produtor.Controllers
{
    [ApiController]
    [Route("/Pedido")]
    public class PedidoController : ControllerBase
    {
        private readonly IBus _bus;
        private readonly IConfiguration _configuration;

        public PedidoController(IBus bus, IConfiguration configuration)
        {
            _bus = bus;
            _configuration = configuration;
        }

        [HttpPost]
        public async Task<IActionResult> Post()
        {
            var nomeFila = _configuration.GetSection("MassTransitAzure")["NomeFila"] ?? string.Empty;
            var endpoint = await _bus.GetSendEndpoint(new Uri($"queue:{nomeFila}"));


            await endpoint.Send(new Pedido(1, new Usuario(5,"Marcelo","marcelo@teste.com")));

            return Ok();
        }
    }
}
