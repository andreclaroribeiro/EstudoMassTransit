using EstudoMassTransit.WebApi.Events;
using MassTransit;
using System.Threading.Tasks;

namespace EstudoMassTransit.WebApi.Consumers
{
    public class ContaCorrenteUpdatedConsumer : IConsumer<ContaCorrenteUpdatedEvent>
    {
        public Task Consume(ConsumeContext<ContaCorrenteUpdatedEvent> context)
        {
            System.Diagnostics.Debug.WriteLine($"Consumer Updated em {System.DateTime.Now} : {context.Message.Descricao}");

            return Task.CompletedTask;
        }
    }
}