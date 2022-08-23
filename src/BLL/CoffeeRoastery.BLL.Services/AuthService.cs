using System;
using System.Threading.Tasks;
using CoffeeRoastery.BLL.Interface.Common;
using CoffeeRoastery.BLL.Interface.Dto;
using CoffeeRoastery.BLL.Interface.Services;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace CoffeeRoastery.BLL.Services;

public class AuthService : IAuthService
{
    private readonly AuthOptions options;
    private readonly ITokenService tokenService;
    private readonly ILogger<AuthService> logger;

    public AuthService(ITokenService tokenService, IOptions<AuthOptions> options,ILogger<AuthService> logger)
    {
        this.options = options.Value;
        this.tokenService = tokenService;
        this.logger = logger;
    }

    public Result<JwtTokenResponse> Authenticate(AuthDto dto)
    {
        logger.LogInformation("{method} called", nameof(Authenticate));
        try
        {
            //in real app password should be hashed ofc
            if (dto.UserName != options.UserName || dto.Password != options.Password)
            {
                const string message = "Unauthorized";
                logger.LogWarning(message);
                return Result.Fail<JwtTokenResponse>(message);
            }

            var tokenResul = tokenService.GenerateJwtToken(dto.UserName);
            if (!tokenResul.IsSucceed)
            {
                return Result.Fail<JwtTokenResponse>(tokenResul.ErrorMessage);
            }
            
            return tokenResul;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Failed to {method}, message: {message}", nameof(Authenticate), ex.Message);
            return Result.Fail<JwtTokenResponse>($"Failed to {nameof(Authenticate)}");
        }
    }
}