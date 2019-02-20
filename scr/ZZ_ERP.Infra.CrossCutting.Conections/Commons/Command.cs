namespace ZZ_ERP.Infra.CrossCutting.Connections.Commons
{
    public class Command
    {
        public string Id { get; set; }
        public long EntityId { get; set; }
        public string Json { get; set; }
        public string Cmd { get; set; }
    }
}
