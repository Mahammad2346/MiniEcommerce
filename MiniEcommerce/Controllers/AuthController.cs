using Microsoft.AspNetCore.Mvc;
using MiniEcommerce.BusinessLogicLayer.Dtos.Auth;
using MiniEcommerce.BusinessLogicLayer.Interfaces;


namespace MiniEcommerce.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController(IAuthService authService) : ControllerBase
    {
        [HttpPost("register")]

        public async Task<IActionResult> Register([FromBody] RegisterRequest request, CancellationToken cancellationToken)
        {
            await authService.RegisterAsync(request);
            return Ok();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest, CancellationToken cancellationToken)
        {
            var token = await authService.LoginAsync(loginRequest, cancellationToken);

            return Ok(new LoginResponse(token));
        }
    }
}
