using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using rawdata_portfolioproject_2;

namespace WebService // also add controllers for other ressources in the same way as below
{
    [ApiController]
    [Route("api/profiles")]
    public class ProfilesController : ControllerBase
    {
        private readonly IDataService _dataService;
        private readonly IConfiguration _configuration;
        //optional: add mapper

        public ProfilesController(IDataService dataService, IConfiguration configuration)
        {
            _dataService = dataService;
            _configuration = configuration;
            //optional add mapper
        }
        
        // make functions like this example:
        [Authorize]
        [HttpGet("email/{email}")]
        public ActionResult<Profile> GetProfile(string email)
        {
            var profile = _dataService.GetProfile(email);

            if (profile == null) return NotFound();
            if (HttpContext.User.Identity.Name != email) return Unauthorized();
            
            return Ok(profile);
        }
        
        /* Example using authentication
        [Authorize]
        [HttpGet]
        public IActionResult GetPosts()
        {
            int.TryParse(HttpContext.User.Identity.Name, out var id);
            var posts = _dataService.GetPosts(id);

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
            
            // authentication stuff
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_configuration["Auth:Key"]);
            var tokenDescription = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    // change type to name if this doesnt work
                    new Claim(ClaimTypes.Name, loginDto.Email),
                }),
                Expires = DateTime.Now.AddMinutes(60),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };

            var securityToken = tokenHandler.CreateToken(tokenDescription);

            var token = tokenHandler.WriteToken(securityToken);

            return Ok(new {loginDto.Email, token});
        }
        
        [HttpPost("delete")]
        public IActionResult DeleteProfile([FromBody] LoginDto loginDto)
        {
            if (!_dataService.DeleteProfile(loginDto.Email, loginDto.Password)) return BadRequest();

            return Ok();
        }
    }
}