using Microsoft.AspNetCore.Mvc;
using UserAuthApiProperArchitecture.Application.DTOs;
using UserAuthApiProperArchitecture.Application.Interfaces;

namespace UserAuthApiProperArchitecture.Api.Controllers
{
    // [ApiController] enables automatic model validation, binding, and error responses 
    // [Route] sets the URL prefix: all endpoints start with /api/auth 
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        // DI:.net provides IUserService automatically
        private readonly IUserService _userService;

        public AuthController(IUserService userService)
        {
            _userService = userService;
        }

        // POST: /api/auth/register
        //[frombody] tells .NET to read the JSON body and deserialize it into RegisterRequestDTO 
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDTO request)
        {
            // [ApiController] already validated the model — if invalid, returns 400 automatically 
            try

            {

                var result = await _userService.RegisterAsync(request);

                // 201 Created — standard HTTP status for resource creation 

                return CreatedAtAction(nameof(Register), result);
            }


            catch (InvalidOperationException ex)

            {

                // 409 Conflict — email or username already exists 

                return Conflict(new { message = ex.Message });

            }
        }
        // POST /api/auth/login 

        [HttpPost("login")]

        public async Task<IActionResult> Login([FromBody] LoginRequestDTO request)

        {

            try

            {

                var result = await _userService.LoginAsync(request);

                // 200 OK — login was successful, return the token 

                return Ok(result);

            }

            catch (UnauthorizedAccessException ex)

            {

                // 401 Unauthorized — bad credentials 

                return Unauthorized(new { message = ex.Message });

            }

        }

    }
}
