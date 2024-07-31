using Api.AppCore.SISS_Integration.Controllers;
using Api.AppCore.SISS_Integration.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Api.AppCore.API_Common.SeveralFunctions
{
    /* --> † 31/07/2024 - Luiz Lenire. <-- */

    public sealed class TokenService
    {
        #region --> Public static properties. <--  

        public const string Siss = "921130442a68c485734fcaf4ca9dddad";

        #endregion --> Public static properties. <--

        #region --> Private properties. <--

        public const string Secret = "7319e279bded0ea012cc16d2baf291bf";

        #endregion --> Private properties. <--

        #region --> Public methods. <--

        public string GenerateToken(ConnectLogin connectLogin)
        {
            SecurityTokenDescriptor securityTokenDescriptor = new()
            {
                Subject = new(new Claim[]
                {
                    new(ClaimTypes.Name, connectLogin.Username.ToString()),
                    new(ClaimTypes.Role, connectLogin.Role.ToString())
                }),
                Expires = DateTime.UtcNow.AddMinutes(60),
                SigningCredentials = new(new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Secret)), SecurityAlgorithms.HmacSha256Signature)
            };

            JwtSecurityTokenHandler jwtSecurityTokenHandler = new();
            SecurityToken securityToken = jwtSecurityTokenHandler.CreateToken(securityTokenDescriptor);

            return jwtSecurityTokenHandler.WriteToken(securityToken);
        }

        public ConnectLogin Get(string username, string password, ref string message)
        {
            Login login = new();
            return login.Get(new(username, password), ref message);
        }
    }

    #endregion --> Public methods. <--   
}

