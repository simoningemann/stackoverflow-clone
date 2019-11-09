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
        
        [Authorize]
        [HttpGet("{profileId}")]
        public ActionResult<Profile> GetProfile(int profileId)
        {
            var profile = _dataService.GetProfile(profileId);

            if (profile == null) return NotFound();
            if (HttpContext.User.Identity.Name != profile.Email) return Unauthorized();

            return Ok(profile);
        }

        [HttpPost]
        public IActionResult CreateProfile([FromBody] LoginDto loginDto)
        {
            var profile = _dataService.CreateProfile(loginDto.Email, loginDto.Password);

            if (profile == null) return BadRequest();
            
            return Created("", profile);
        }

        [Authorize]
        [HttpPut("updatepassword")]
        public IActionResult UpdatePassword([FromBody] UpdatePasswordDto updatePasswordDto)
        {
            if (!_dataService.UpdateProfilePassword(updatePasswordDto.Email, updatePasswordDto.OldPassword,
                updatePasswordDto.NewPassword)) return BadRequest();
            return Ok();
        }
        
        [Authorize]
        [HttpPut("updateemail")]
        public IActionResult UpdateEmail([FromBody] UpdateEmailDto updateEmailDto)
        {
            if (!_dataService.UpdateProfileEmail(updateEmailDto.OldEmail, updateEmailDto.Password,
                updateEmailDto.NewEmail)) return BadRequest();

            //logout
            
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
        
        [Authorize]
        [HttpPost("delete")]
        public IActionResult DeleteProfile([FromBody] LoginDto loginDto)
        {
            if (!_dataService.DeleteProfile(loginDto.Email, loginDto.Password)) return BadRequest();

            return Ok();
        }
    }
}