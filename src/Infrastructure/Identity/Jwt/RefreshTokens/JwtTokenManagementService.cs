using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using CommandsRegistry.Application.Authentication;
using CommandsRegistry.Application.Authentication.DTOs;
using CommandsRegistry.Infrastructure.Identity.Jwt.Claims;
using CommandsRegistry.Infrastructure.Identity.Jwt.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace CommandsRegistry.Infrastructure.Identity.Jwt.RefreshTokens
{
    internal sealed class JwtTokenManagementService : IJwtTokenManagementService
    {
        private readonly JwtSecurityTokenHandler jwtSecurityTokenHandler;
        private readonly ITokenConfiguration tokenConfiguration;

        public JwtTokenManagementService(ITokenConfiguration tokenConfiguration)
        {
            this.tokenConfiguration = tokenConfiguration;
            jwtSecurityTokenHandler ??= new JwtSecurityTokenHandler();
        }

        public Guid GetUserIdFromToken(string accessToken)
        {
            var principal = jwtSecurityTokenHandler.ValidateToken(accessToken, new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(tokenConfiguration.Secret)),
                //ValidIssuer = tokenConfiguration.Issuer,
                //ValidAudience = tokenConfiguration.Audience,
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = false // we do not validate lifetime - token can be expired and we will generate new one based on refresh token
            }, out var securityToken);

            if (securityToken is not JwtSecurityToken jwtSecurityToken ||
                !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
            {
                throw new SecurityTokenException("Invalid token");
            }

            var userId = principal.Claims.Single(c => c.Type == ClaimTypes.NameIdentifier).Value;
            return Guid.Parse(userId);
        }

        public string GenerateJwtToken(UserDto userDetails, IList<string> roles)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, userDetails.Id.ToString()),
                //new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.Email, userDetails.Email),
                new Claim(CustomClaimTypes.FirstName, !string.IsNullOrEmpty(userDetails.FirstName) ? userDetails.FirstName : ""),
                new Claim(CustomClaimTypes.LastName, !string.IsNullOrEmpty(userDetails.LastName) ? userDetails.LastName : ""),
            };

            foreach (var role in roles)
                claims.Add(new Claim(ClaimTypes.Role, role));

            var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(tokenConfiguration.Secret));
            var signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(tokenConfiguration.Issuer,
                tokenConfiguration.Audience,
                claims,
                expires: DateTime.Now.AddMinutes(tokenConfiguration.AccessExpirationInMinutes),
                signingCredentials: signingCredentials
            );

            return jwtSecurityTokenHandler.WriteToken(token);
        }
    }
}