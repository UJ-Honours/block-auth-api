using block_auth_api.Models;
using block_auth_api.Orchestration.UsersContract;
using Microsoft.Extensions.Configuration;
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
        private readonly IConfiguration _Config;
        private readonly IUsersContractOrchestration _UCO;

        public TokenOrchestration(IConfiguration config, IUsersContractOrchestration uco)
        {
            _Config = config;
            _UCO = uco;
        }
        public string BuildToken(UserVM user)
        {
            var claims = new[] {
            new Claim(JwtRegisteredClaimNames.Sub,
                      user.Account),
            new Claim(
                JwtRegisteredClaimNames.GivenName,
                user.Name),
            new Claim(JwtRegisteredClaimNames.Email, user.Email),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_Config["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_Config["Jwt:Issuer"],
                                             _Config["Jwt:Audience"],
                                             claims,
                                             expires: DateTime.Now.AddMinutes(30),
                                             signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }


        public User Authenticate(UserVM login)
        {
            // TODO: This method will authenticate the user recovering his Ethereum address through underlaying offline ecrecover method.

            var userList = _UCO.GetUsers();

            return userList.FirstOrDefault(x => x.Name == login.Name);
        }
    }
}
