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
        public ActionResult CreateProfile([FromBody] ProfileForCreation dto)
        {
            int.TryParse(_configuration.GetSection("Auth:PwdSize").Value, out var size);

            if (size == 0) return BadRequest("Hash size must be bigger than 0.");
                
            var salt = PasswordService.GenerateSalt(size);
            var hash = PasswordService.HashPassword(dto.Password, salt, size);
            var profile = _profileService.CreateProfile(dto.Email, salt, hash);

            if (profile == null) return BadRequest(new {error= "Non-unique email"});
            
            return Created("", new
            {
                profile.ProfileId,
                profile.Email
            });
        }
        
        [HttpPost("login", Name = nameof(ProfileLogin))]
        public ActionResult ProfileLogin([FromBody] ProfileForLogin dto)
        {
            var profile = _profileService.GetProfile(dto.Email);
            
            if (profile == null) return NotFound("Profile not found");
            
            int.TryParse(
                _configuration.GetSection("Auth:PwdSize").Value, 
                out var size);

            if (size == 0) return BadRequest("Hash size should be bigger than 0");
            
            var hash = PasswordService.HashPassword(dto.Password, profile.Salt, size);

            if (hash != profile.Hash) return BadRequest("Wrong password.");

            var token = CreateToken(profile.ProfileId, 30);

            return Ok(new
            {
                profile.ProfileId,
                profile.Email,
                token
            });
            
        }

        [Authorize]
        [HttpPost("delete", Name = nameof(DeleteProfile))]
        public ActionResult DeleteProfile([FromBody] ProfileForDeletion dto)
        {
            int.TryParse(HttpContext.User.Identity.Name, out var profileId);
            var profile = _profileService.GetProfile(profileId);

            if (profile == null) return NotFound();
            
            int.TryParse(
                _configuration.GetSection("Auth:PwdSize").Value, 
                out var size);

            if (size == 0) return BadRequest("Hash size should be bigger than 0");

            var hash = PasswordService.HashPassword(dto.Password, profile.Salt, size);

            if (hash != profile.Hash) return BadRequest("Wrong password.");

            var deletedProfile = _profileService.DeleteProfile(profileId);

            if (deletedProfile == null) return BadRequest("Error on deleting profile");
            
            // preferably end session here

            return Ok("Deleted: " + deletedProfile.Email);
        }

        [Authorize]
        [HttpPut("updatepassword", Name = nameof(UpdateProfilePassword))]
        public ActionResult UpdateProfilePassword([FromBody] PasswordForUpdate dto)
        {
            int.TryParse(HttpContext.User.Identity.Name, out var profileId);
            var profile = _profileService.GetProfile(profileId);

            if (profile == null) return NotFound();
            
            int.TryParse(
                _configuration.GetSection("Auth:PwdSize").Value, 
                out var size);

            if (size == 0) return BadRequest("Hash size should be bigger than 0");

            var oldHash = PasswordService.HashPassword(dto.OldPassword, profile.Salt, size);

            if (profile.Hash != oldHash) return Unauthorized("Wrong password");

            var newHash = PasswordService.HashPassword(dto.NewPassword, profile.Salt, size);

            var updatedProfile = _profileService.UpdateProfilePassword(profileId, newHash);

            if (updatedProfile == null) return BadRequest("Error on updating profile");

            return Ok("Updated password for: " + updatedProfile.Email);
        }
        
        [Authorize]
        [HttpPut("updateemail", Name = nameof(UpdateProfileEmail))]
        public ActionResult UpdateProfileEmail([FromBody] EmailForUpdate dto)
        {
            int.TryParse(HttpContext.User.Identity.Name, out var profileId);
            var profile = _profileService.GetProfile(profileId);

            if (profile == null) return NotFound();
            
            int.TryParse(
                _configuration.GetSection("Auth:PwdSize").Value, 
                out var size);

            if (size == 0) return BadRequest("Hash size should be bigger than 0");

            var hash = PasswordService.HashPassword(dto.Password, profile.Salt, size);

            if (profile.Hash != hash) return Unauthorized("Wrong password");

            var updatedProfile = _profileService.UpdateProfileEmail(profileId, dto.NewEmail);

            if (updatedProfile == null) return BadRequest("Error on updating profile");

            return Ok("Updated email to: " + updatedProfile.Email);
        }
        
        private string CreateToken(int profileId, int timeValid)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_configuration["Auth:Key"]);
            var tokenDescription = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, profileId.ToString()),
                }),
                Expires = DateTime.Now.AddMinutes(timeValid),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };

            var securityToken = tokenHandler.CreateToken(tokenDescription);

            return tokenHandler.WriteToken(securityToken);
        }
    }
}