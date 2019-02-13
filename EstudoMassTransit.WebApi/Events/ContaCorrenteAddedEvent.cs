namespace EstudoMassTransit.WebApi.Events
{
    public class ContaCorrenteAddedEvent
    {
        public int Numero { get; set; }
        public int Digito { get; set; }
        public string Descricao { get; set; }
    }
}