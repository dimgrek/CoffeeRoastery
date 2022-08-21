using System.Threading.Tasks;
using CoffeeRoastery.BLL.Interface.Common;
using CoffeeRoastery.BLL.Interface.Dto;

namespace CoffeeRoastery.BLL.Interface.Services;

public interface IAuthService
{
    Task<Result<JwtTokenResponse>> Authenticate(AuthDto dto);
}