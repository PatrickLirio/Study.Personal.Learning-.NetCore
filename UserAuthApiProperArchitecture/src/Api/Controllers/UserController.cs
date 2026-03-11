using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using UserAuthApiProperArchitecture.Domain.Entities;

namespace UserAuthApiProperArchitecture.Api.Controllers
{
    public class UserController : ControllerBase
    {
        [Authorize]
        [HttpGet("profile")]
        public IActionResult GetProfile()

        {
            // User claims are extracted from the JWT automatically 

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var email = User.FindFirstValue(ClaimTypes.Email);

            var username = User.FindFirstValue(ClaimTypes.Name);

            var role = User.FindFirstValue(ClaimTypes.Role);

            return Ok(new { userId, email, username, role });

        }
    }
}
