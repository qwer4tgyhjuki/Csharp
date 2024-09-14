using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using WebApplication2.Models;
using WebApplication2.Services.ApiService.ApiService;
using WebApplication2.Services.AuthService;
using WebApplication2.Services.UserService;

namespace WebApplication2.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IUserService _userService;



        public AuthController(IAuthService authService, IUserService userService)
        {
            _authService = authService;
            _userService = userService;
        }

        [HttpPost("Register")]
        public async Task<ActionResult> Register(UserModel newUser)
        {
            try
            {
                var existingUser = _userService.GetUsername(newUser.Username, newUser.Email);
                if (existingUser)
                {
                    return BadRequest("User with these username or email already exists.");
                }
                var user = await _authService.RegisterUser(newUser);
                _userService.AddUser(user);
                return Ok(user);

            } catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("Login")]
        public async Task<ActionResult> Login(LoginModel loginModel)
        {
            try
            {
                var validUser = await _userService.GetUserByName(loginModel);
                Console.WriteLine(loginModel.Username);
                if (!validUser)
                {
                    return Unauthorized("Invalid username or password");
                }
                var token = _authService.GenerateJwtToken(loginModel.Username);

                if (token == null)
                {
                    return Unauthorized("Invalid Attempt...");
                }

                return Ok(token);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
