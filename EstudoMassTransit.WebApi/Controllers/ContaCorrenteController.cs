using EstudoMassTransit.WebApi.Events;
using MassTransit;
using Microsoft.AspNetCore.Mvc;

namespace EstudoMassTransit.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContaCorrenteController : ControllerBase
    {
        private readonly IBus _bus;

        public ContaCorrenteController(IBus bus)
        {
            _bus = bus;
        }

        [HttpPost]
        public void Post([FromBody] string value)
        {
            _bus.Publish(
                new ContaCorrenteAddedEvent
                {
                    Numero = 1,
                    Digito = 2,
                    Descricao = value
                });
        }

        [HttpPut]
        public void Put([FromBody] string value)
        {
            _bus.Publish(
                new ContaCorrenteUpdatedEvent
                {
                    Numero = 3,
                    Digito = 3,
                    Descricao = value
                });
        }
    }
}