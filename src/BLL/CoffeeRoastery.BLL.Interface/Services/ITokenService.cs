using CoffeeRoastery.BLL.Interface.Common;
using CoffeeRoastery.BLL.Interface.Dto;

namespace CoffeeRoastery.BLL.Interface.Services;

public interface ITokenService
{
    Result<JwtTokenResponse> GenerateJwtToken(string userName);
}