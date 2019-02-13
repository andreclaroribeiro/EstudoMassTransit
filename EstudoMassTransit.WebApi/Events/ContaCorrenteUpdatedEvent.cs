namespace EstudoMassTransit.WebApi.Events
{
    public class ContaCorrenteUpdatedEvent
    {
        public int Numero { get; set; }
        public int Digito { get; set; }
        public string Descricao { get; set; }
    }
}