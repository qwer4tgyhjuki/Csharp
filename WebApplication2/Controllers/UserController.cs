using Microsoft.AspNetCore.Mvc;
using WebApplication2.Models;
using WebApplication2.Services.UserService;

namespace WebApplication2.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("GetAllUsers")]
        public async Task<ActionResult> GetAllUsers() 
        {
            try
            {
                var users = await _userService.GetAllUsers();
                return Ok(users);
            } catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetUserById")]
        public async Task<ActionResult> GetUserById(int id)
        {
            try
            {
                var user = await _userService.GetUserById(id);
                return Ok(user);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("UpdateUser")]
        public async Task<ActionResult> UpdateUser(int id, UserModel userModel)
        {
            try
            {
                var updatedUser = await _userService.UpdateUser(id, userModel);
                return Ok(new { Message = "Updated", updatedUser = updatedUser });
            } catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("DeleteUserById")]
        public async Task<ActionResult> DeleteUserById(int id)
        {
            try
            {
                var users = await _userService.DeleteUserById(id);
                return Ok(new {Message = "Deleted", users = users});
            } catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("AddUser")]
        public async Task<ActionResult> AddUser(UserModel userModel)
        {
            try
            {
                await _userService.AddUser(userModel);
                return Ok(userModel);
            } catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
