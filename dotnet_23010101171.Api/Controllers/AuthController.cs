using dotnet_23010101171.Application.Repository;
using dotnet_23010101171.Core.DTOs.Auth;
using Microsoft.AspNetCore.Mvc;

namespace dotnet_23010101171.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(IAuthService authService) : ControllerBase
    {
        [HttpPost("login")]
        public async Task<ActionResult<LoginResponse>> Login([FromBody] LoginRequests request, CancellationToken cancellationToken)
        {
            var response = await authService.LoginAsync(request, cancellationToken);
            if (response is null)
                return Unauthorized("Invalid email or password.");

            return Ok(response);
        }
    }
}
