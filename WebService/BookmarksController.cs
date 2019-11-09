using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using rawdata_portfolioproject_2;

namespace WebService // also add controllers for other ressources in the same way as below
{
    [ApiController]
    [Route("api/bookmarks")]
    public class BookmarksController : ControllerBase
    {
        private readonly IDataService _dataService;
        private readonly IConfiguration _configuration;
        //optional: add mapper

        public BookmarksController(IDataService dataService, IConfiguration configuration)
        {
            _dataService = dataService;
            _configuration = configuration;
            //optional add mapper
        }

        [Authorize]
        [HttpGet("{profileId}")]
        public IActionResult GetAllBookmarks(int profileId)
        {
            if (HttpContext.User.Identity.Name != _dataService.GetProfile(profileId).Email) return Unauthorized();

            var bookmarks = _dataService.GetAllBookmarks(profileId);

            var tags = new {tags = bookmarks};

            return Ok(JsonConvert.SerializeObject(tags)); // is this correct?
        }
        
        [Authorize]
        [HttpGet("update")]
        public IActionResult UpdateBookmark([FromBody] UpdateBookmerkDto dto)
        {
            if (HttpContext.User.Identity.Name != _dataService.GetProfile(dto.ProfileId).Email) return Unauthorized();
            
            if (!_dataService.UpdateBookmark(dto.ProfileId, dto.BookmarkId, dto.Note)) return NotFound();

            return Ok(); 
        }
    }
}