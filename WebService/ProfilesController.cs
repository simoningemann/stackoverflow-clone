using Microsoft.AspNetCore.Mvc;
using rawdata_portfolioproject_2;

namespace WebService // also add controllers for other ressources in the same way as below
{
    [ApiController]
    [Route("api/profiles")]
    public class ProfilesController : Controller
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
        
        [HttpPost]
        public IActionResult CreateProfile([FromBody] LoginInfo loginInfo)
        {
            var profile = _dataService.CreateProfile(loginInfo.Email, loginInfo.Password);

            if (profile == null) return BadRequest();
            
            return Created("", profile);
        }
        
        [HttpPost("delete")]
        public ActionResult DeleteProfile([FromBody] LoginInfo loginInfo)
        {
            if (!_dataService.DeleteProfile(loginInfo.Email, loginInfo.Password)) return BadRequest();

            return Ok();
        }
    }
}