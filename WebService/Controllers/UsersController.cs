using Microsoft.AspNetCore.Mvc;
using rawdata_portfolioproject_2.Services;
using rawdata_portfolioproject_2.Services.Interfaces;

namespace WebService.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }
        
        [HttpGet("{userId}", Name = nameof(GetUser))]
        public ActionResult GetUser(int userId)
        {
            var user = _userService.GetUser(userId);

            if (user == null) return NotFound();
            
            return Ok(user);
        }

        [HttpGet(Name = nameof(GetUsers))]
        public ActionResult GetUsers([FromQuery] int[] userIds)
        {
            var users = _userService.GetUsers(userIds);

            return Ok(users);
        }
    }
}