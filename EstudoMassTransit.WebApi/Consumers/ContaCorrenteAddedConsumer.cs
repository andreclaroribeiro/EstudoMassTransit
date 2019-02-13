using EstudoMassTransit.WebApi.Events;
using MassTransit;
using System.Threading.Tasks;

namespace EstudoMassTransit.WebApi.Consumers
{
    public class ContaCorrenteAddedConsumer : IConsumer<ContaCorrenteAddedEvent>
    {
        public async Task Consume(ConsumeContext<ContaCorrenteAddedEvent> context)
        {
            System.Diagnostics.Debug.WriteLine($"Consumer Added em {System.DateTime.Now} : {context.Message.Descricao}");

            await Task.CompletedTask;
        }
    }
}