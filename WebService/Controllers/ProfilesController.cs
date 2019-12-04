using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using rawdata_portfolioproject_2.Models;
using rawdata_portfolioproject_2.Services;
using rawdata_portfolioproject_2.Services.Interfaces;
using WebService.Models;

namespace WebService.Controllers // also add controllers for other ressources in the same way as below
{
    [ApiController]
    [Route("api/profiles")]
    public class ProfilesController : ControllerBase
    {
        private readonly IProfileService _profileService;
        private readonly IConfiguration _configuration;
        //optional: add mapper

        public ProfilesController(IProfileService profileService, IConfiguration configuration)
        {
            _profileService = profileService;
            _configuration = configuration;
            //optional add mapper
        }
        
        [HttpPost(Name = nameof(CreateProfile))]
        public ActionResult CreateProfile([FromBody] LoginDto loginDto)
        {
            int.TryParse(_configuration.GetSection("Auth:PwdSize").Value, out var size);

            if (size == 0) return BadRequest("Hash size must be bigger than 0.");
                
            var salt = PasswordService.GenerateSalt(size);
            var hash = PasswordService.HashPassword(loginDto.Password, salt, size);
            var profile = _profileService.CreateProfile(loginDto.Email, salt, hash);

            if (profile == null) return BadRequest("Non-unique email");
            
            return Created("", profile.Email);
        }
        
        [HttpPost("login")]
        public ActionResult ProfileLogin([FromBody] LoginDto dto)
        {
            var profile = _profileService.GetProfile(dto.Email);
            
            if (profile == null) return NotFound("Profile not found");
            
            int.TryParse(
                _configuration.GetSection("Auth:PwdSize").Value, 
                out var size);

            if (size == 0) return BadRequest("Hash size should be bigger than 0");
            
            var hash = PasswordService.HashPassword(dto.Password, profile.Salt, size);

            if (hash != profile.Hash) return BadRequest("Wrong password.");

            var token = CreateToken(profile.Email, 5);

            return Ok(new {profile.Email, token});
        }

        [Authorize]
        [HttpPost("delete", Name = nameof(DeleteProfile))]
        public ActionResult DeleteProfile([FromBody] DeleteProfileDto dto)
        {
            var email = HttpContext.User.Identity.Name;
            var profile = _profileService.GetProfile(email);

            if (profile == null) return NotFound();
            
            int.TryParse(
                _configuration.GetSection("Auth:PwdSize").Value, 
                out var size);

            if (size == 0) return BadRequest("Hash size should be bigger than 0");

            var hash = PasswordService.HashPassword(dto.Password, profile.Salt, size);

            if (hash != profile.Hash) return BadRequest("Wrong password.");

            var deletedProfile = _profileService.DeleteProfile(email);

            if (deletedProfile == null) return BadRequest("Error on deleting profile");
            
            // preferable end session here

            return Ok("Deleted: " + deletedProfile.Email);
        }

        [Authorize]
        [HttpPut("updatepassword")]
        public IActionResult UpdatePassword([FromBody] UpdatePasswordDto dto)
        {
            var email = HttpContext.User.Identity.Name;
            var profile = _profileService.GetProfile(email);

            if (profile == null) return NotFound();
            
            int.TryParse(
                _configuration.GetSection("Auth:PwdSize").Value, 
                out var size);

            if (size == 0) return BadRequest("Hash size should be bigger than 0");

            var oldHash = PasswordService.HashPassword(dto.OldPassword, profile.Salt, size);

            if (profile.Hash != oldHash) return Unauthorized("Wrong password");

            var newHash = PasswordService.HashPassword(dto.NewPassword, profile.Salt, size);

            var updatedProfile = _profileService.UpdateProfilePassword(email, newHash);

            if (updatedProfile == null) return BadRequest("Error on updating profile");

            return Ok("Updated password for: " + updatedProfile.Email);
        }
        
        private string CreateToken(string email, int timeValid)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_configuration["Auth:Key"]);
            var tokenDescription = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, email),
                }),
                Expires = DateTime.Now.AddMinutes(timeValid),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };

            var securityToken = tokenHandler.CreateToken(tokenDescription);

            return tokenHandler.WriteToken(securityToken);
        }

        /*

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
        }*/
    }
}