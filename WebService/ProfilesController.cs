using Microsoft.AspNetCore.Mvc;
using rawdata_portfolioproject_2;

namespace WebService // also add controllers for other ressources in the same way as below
{
    [ApiController]
    [Route("api/profiles")]
    public class ProfilesController : ControllerBase
    {
        private IDataService _dataService;
        // later add mapper.

        public ProfilesController(IDataService dataService)
        {
            _dataService = dataService;
            // later add mapper
        }
        
        // make functions like this example:
        [HttpGet("email/{email}")]
        public ActionResult<Profile> GetProfile(string email)
        {
            var profile = _dataService.GetProfile(email);

            if (profile == null) return NotFound();
            
            return Ok(profile);
        }
        
        /* Example using authentication
        [HttpGet]
        public IActionResult GetPosts()
        {
            if (Program.CurrentUser == null)
                return Unauthorized();

            var posts = _dataService.GetPosts(Program.CurrentUser.Id);

            var result = posts.Select(x => new PostDto { Title = x.Title });
            return Ok(result);
        }
        */
        
        [HttpPost]
        public IActionResult CreateProfile([FromBody] LoginDto loginDto)
        {
            var profile = _dataService.CreateProfile(loginDto.Email, loginDto.Password);

            if (profile == null) return BadRequest();
            
            return Created("", profile);
        }

        [HttpPut("updatepassword")]
        public IActionResult UpdatePassword([FromBody] UpdatePasswordDto updatePasswordDto)
        {
            if (!_dataService.UpdateProfilePassword(updatePasswordDto.Email, updatePasswordDto.OldPassword,
                updatePasswordDto.NewPassword)) return BadRequest();
            return Ok();
        }
        
        [HttpPut("updateemail")]
        public IActionResult UpdateEmail([FromBody] UpdateEmailDto updateEmailDto)
        {
            if (!_dataService.UpdateProfileEmail(updateEmailDto.OldEmail, updateEmailDto.Password,
                updateEmailDto.NewEmail)) return BadRequest();
            
            return Ok();
        }
        
        [HttpPost("login")]
        public IActionResult ProfileLogin([FromBody] LoginDto loginDto)
        {
            if (!_dataService.Login(loginDto.Email, loginDto.Password)) return BadRequest();

            return Ok();
        }
        
        [HttpPost("delete")]
        public IActionResult DeleteProfile([FromBody] LoginDto loginDto)
        {
            if (!_dataService.DeleteProfile(loginDto.Email, loginDto.Password)) return BadRequest();

            return Ok();
        }
    }
}