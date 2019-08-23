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

        public string BuildToken(User user)
        {
            var claims = new[] {
                new Claim(JwtRegisteredClaimNames.Sub,user.Account),
                new Claim(JwtRegisteredClaimNames.GivenName,user.Username),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var jwtKey = _Config["Jwt:Key"];
            var issuer = _Config["Jwt:Issuer"];
            var audience = _Config["Jwt:Audience"];

            var encodingBytes = Encoding.UTF8.GetBytes(jwtKey);
            var key = new SymmetricSecurityKey(encodingBytes);
            var securityAlgorithm = SecurityAlgorithms.HmacSha256;
            var creds = new SigningCredentials(key, securityAlgorithm);

            var token = new JwtSecurityToken(issuer,
                                             audience,
                                             claims,
                                             expires: DateTime.Now.AddMinutes(30),
                                             signingCredentials: creds);

            return new JwtSecurityTokenHandler()
                .WriteToken(token);
        }

        public User Authenticate(User user)
        {
            // TODO: This method will authenticate the user recovering his Ethereum address through underlaying offline ecrecover method.
            var userList = _UCO.GetUsers();
            return userList
                .FirstOrDefault(x => x.Username == user.Username);
        }
    }
}