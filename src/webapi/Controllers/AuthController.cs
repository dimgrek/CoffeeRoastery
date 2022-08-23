using System.Threading.Tasks;
using AutoMapper;
using CoffeeRoastery.BLL.Interface.Dto;
using CoffeeRoastery.BLL.Interface.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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
    /// <summary>
    /// Endpoint to authorize your requests
    /// </summary>
    /// <remarks>
    /// In order to use ProductController, valid credentials should be presented. Ask them from your barista. 
    /// </remarks>
    [HttpPost]
    [AllowAnonymous]
    [ProducesResponseType(typeof(JwtTokenResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
    public IActionResult Authenticate(AuthModel model)
    {
        return authService.Authenticate(mapper.Map<AuthDto>(model)).ToActionResult();
    }
}