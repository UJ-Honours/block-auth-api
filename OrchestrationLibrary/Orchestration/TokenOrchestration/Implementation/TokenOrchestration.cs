using block_auth_api.Models;
using block_auth_api.Orchestration.UsersContract;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace block_auth_api.Orchestration.TokenOrchestration
{
    public class TokenOrchestration : ITokenOrchestration
    {
        private readonly JWTSettings _JWTSettings;
        private readonly IUsersContractOrchestration _UCO;

        public TokenOrchestration(IOptions<JWTSettings> jwtSettings, IUsersContractOrchestration uco)
        {
            _JWTSettings = jwtSettings.Value;
            _UCO = uco;
        }

        public string BuildToken(User user)
        {
            // authentication successful so generate jwt token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_JWTSettings.SecretKey);
            var signinCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(
                    new Claim[]
                    {
                        new Claim(ClaimTypes.Name, user.Username.ToString())
                    }
                ),
                Expires = DateTime.UtcNow.AddHours(2),
                SigningCredentials = signinCredentials
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return new JwtSecurityTokenHandler()
                .WriteToken(token);
        }

        public User Authenticate(User user)
        {
            // TODO: This method will authenticate the user recovering his Ethereum address through underlaying offline ecrecover method.
            var userList = _UCO.GetUsers();
            var validUser = userList
                .FirstOrDefault(x => (x.Username == user.Username) && (x.Password == user.Password));
            validUser.Password = "";
            return validUser;
        }
    }
}