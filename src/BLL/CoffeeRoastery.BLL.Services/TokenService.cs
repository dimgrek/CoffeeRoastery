using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using CoffeeRoastery.BLL.Interface.Common;
using CoffeeRoastery.BLL.Interface.Dto;
using CoffeeRoastery.BLL.Interface.Services;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using webapi.Options;

namespace CoffeeRoastery.BLL.Services;

public class TokenService : ITokenService
{
    private readonly JWTTokenOptions options;
    private readonly ILogger<TokenService> logger;

    public TokenService(IOptions<JWTTokenOptions> options, ILogger<TokenService> logger)
    {
        this.options = options.Value;
        this.logger = logger;
    }

    public Result<JwtTokenResponse> GenerateJwtToken(string userName)
    {
        logger.LogInformation("{method} called", nameof(GenerateJwtToken));

        try
        {
            // generate token that is valid for 1 hour
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(options.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("userName", userName) }),
                Expires = DateTime.UtcNow.AddHours(1),
                Audience = options.Audience,
                Issuer = options.Issuer,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var response = new JwtTokenResponse
            {
                AccessToken = tokenHandler.WriteToken(token),
                ExpiresIn = TimeSpan.FromHours(1).TotalSeconds,
                TokenType = "Bearer"
            };
        
            return Result.Ok(response);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Failed to {method}, message: {message}", nameof(GenerateJwtToken), ex.Message);
            return Result.Fail<JwtTokenResponse>($"Failed to {nameof(GenerateJwtToken)}");
        }
    }
}