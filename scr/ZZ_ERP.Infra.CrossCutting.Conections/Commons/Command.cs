namespace ZZ_ERP.Infra.CrossCutting.Connections.Commons
{
    public class  Command
    {
        public string Id { get; set; }
        public long EntityId { get; set; }
        public string Json { get; set; }
        public string Cmd { get; set; }
        public bool IsWait { get; set; }

        public Command()
        { }

        public Command(Command cmd)
        {
            Id = cmd.Id;
            EntityId = cmd.EntityId;
            Json = cmd.Json;
            Cmd = cmd.Cmd;
            IsWait = cmd.IsWait;
        }
        
    }
}
