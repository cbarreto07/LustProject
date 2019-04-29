using System.Security.Cryptography;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace Lust.Infra.CrossCutting.Identity.Authorization
{
    public class SigningConfigurations
    {
        private const string SecretKey = "supersecuritykey@lust.com.br";
        public SecurityKey Key { get; }
        public SigningCredentials SigningCredentials { get; }

        public SigningConfigurations()
        {
            //using (var provider = new RSACryptoServiceProvider(2048))
            //{
            //    Key = new RsaSecurityKey(provider.ExportParameters(true));
            //}

            Key =new SymmetricSecurityKey(Encoding.ASCII.GetBytes(SecretKey));

            //SigningCredentials = new SigningCredentials(
            //    Key, SecurityAlgorithms.RsaSha256Signature);

            SigningCredentials = new SigningCredentials(Key, SecurityAlgorithms.HmacSha256);
        }
    }
}
