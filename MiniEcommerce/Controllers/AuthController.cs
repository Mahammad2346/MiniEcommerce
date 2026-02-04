using Microsoft.AspNetCore.Mvc;
using MiniEcommerce.BusinessLogicLayer.Services;
using MiniEcommerce.BusinessLogicLayer.Dtos.Auth;


namespace MiniEcommerce.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController(AuthService authService, JwtTokenService jwtTokenService) : ControllerBase
    {
        [HttpPost("register")]

        public async Task<IActionResult> Register([FromBody] RegisterRequest request, CancellationToken cancellationToken)
        {
            await authService.RegisterAsync(request, cancellationToken);
            return Ok();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest, CancellationToken cancellationToken)
        {
            var user = await authService.LoginAsync(loginRequest, cancellationToken);
            var token = jwtTokenService.GenerateToken(user);
            return Ok(new LoginResponse(token));
        }
    }
}
