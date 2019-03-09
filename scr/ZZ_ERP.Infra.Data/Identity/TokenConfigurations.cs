namespace ZZ_ERP.Infra.Data.Identity
{
    public class TokenConfigurations
    {
        public string Audience { get; set; }
        public string Issuer { get; set; }
        public int Seconds { get; set; }

        private static TokenConfigurations _token;

        public static TokenConfigurations Instance
        {
            get
            {
                if (_token == null)
                {
                    _token = new TokenConfigurations();
                }

                return _token;
            }
        }

        public TokenConfigurations()
        {
            Audience = "ZZAudience";
            Issuer = "ZZIssuer";
            Seconds = 600;
        }
    }
}
