using System.Security.Cryptography;
using Microsoft.IdentityModel.Tokens;

namespace ZZ_ERP.Infra.Data.Identity
{
    public class SigningConfigurations
    {
        public SecurityKey Key { get; }
        public SigningCredentials SigningCredentials { get; }
        private static SigningConfigurations _instance;

        public static SigningConfigurations Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new SigningConfigurations();
                }
                return _instance;
            }
        }


        public SigningConfigurations()
        {
            using (var provider = new RSACryptoServiceProvider(2048))
            {
                Key = new RsaSecurityKey(provider.ExportParameters(true));
            }
             
            SigningCredentials = new SigningCredentials(
                Key, SecurityAlgorithms.RsaSha256Signature);
        }
    }
}
