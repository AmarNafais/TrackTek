using Data.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services;
namespace Web.Controllers;

[ApiController]
[Authorize]
public class UserController : Controller
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [Route("v1/User/GetUserById")]
    [HttpGet]
    public IActionResult GetUserById(int userId)
    {
        try
        {
            var user = _userService.GetUserById(userId);
            return Ok(user);
        }
        catch (Exception ex)
        {
            return BadRequest(ex);
        }
    }

    [Route("v1/User/GetAllUsers")]
    [HttpGet]
    public IActionResult GetAllUsers()
    {
        try
        {
            var users = _userService.GetAllUsers();
            return Ok(users);
        }
        catch (Exception ex)
        {
            return BadRequest(ex);
        }
    }

    [Route("v1/User/UpdateUser")]
    [HttpPost]
    public IActionResult UpdateUser(User user)
    {
        try
        {
            _userService.UpdateUser(user);
            return Ok("User updated successfully");
        }
        catch (Exception ex)
        {
            return NotFound(ex.Message);
        }
    }
}
