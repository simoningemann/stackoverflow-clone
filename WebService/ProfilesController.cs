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
        [HttpGet("profileId")]
        public ActionResult<Profile> GetProfile(int profileId)
        {
            return Ok(/*GetProfile(profileId)*/);
        }
    }
}