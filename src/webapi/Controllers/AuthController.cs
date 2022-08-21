using System.Threading.Tasks;
using AutoMapper;
using CoffeeRoastery.BLL.Interface.Dto;
using CoffeeRoastery.BLL.Interface.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using webapi.Extensions;
using webapi.Models;

namespace webapi.Controllers;

[ApiController]
[Route("auth")]
public class AuthController : ControllerBase
{
    private readonly IAuthService authService;
    private readonly IMapper mapper;

    public AuthController(IAuthService authService, IMapper mapper)
    {
        this.authService = authService;
        this.mapper = mapper;
    }

    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> Authenticate(AuthModel model)
    {
        return await authService.Authenticate(mapper.Map<AuthDto>(model)).ToActionResult();
    }
}