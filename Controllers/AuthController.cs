using BooksCatalogAPI.Models.Login;
using Microsoft.AspNetCore.Mvc;

namespace BooksCatalogAPI.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        [HttpPost("login")]
        public ActionResult<LoginRequest> Login(LoginRequest request)
        {
            if (request.Email == "user@test.com" && request.Password == "Password123")
            {
                return Ok(new LoginResponse
                {
                    Success = true,
                    Message = "Login successful",
                    UserId = 1
                });
            }

            return Unauthorized(new LoginResponse
            {
                Success = false,
                Message = "Invalid Credentials"
            });
        }
    }
}
