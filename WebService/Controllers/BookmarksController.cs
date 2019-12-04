using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using rawdata_portfolioproject_2.Models;
using rawdata_portfolioproject_2.Services;
using rawdata_portfolioproject_2.Services.Interfaces;
using WebService.Models;

namespace WebService.Controllers // also add controllers for other ressources in the same way as below
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
        [HttpGet(Name = nameof(GetBookmarks))]
        public IActionResult GetBookmarks()
        {
            if (HttpContext.User.Identity.Name != _dataService.GetProfile(profileId).Email) return Unauthorized();

            var bookmarks = _dataService.GetAllBookmarks(profileId);

            var tags = new {tags = bookmarks};

            return Ok(JsonConvert.SerializeObject(tags)); // is this correct?
        }
        
        [Authorize]
        [HttpPost]
        public IActionResult CreateBookmark([FromBody] CreateOrUpdateBookmarkDto dto)
        {
            if (HttpContext.User.Identity.Name != _dataService.GetProfile(dto.ProfileId).Email) return Unauthorized();

            var bookmark = _dataService.CreateBookmark(dto.ProfileId, dto.BookmarkId, dto.Note);
            
            if (bookmark == null) return BadRequest();

            return Ok(bookmark); // is return type ok?
        }
        
        [Authorize]
        [HttpPut]
        public IActionResult UpdateBookmark([FromBody] CreateOrUpdateBookmarkDto dto)
        {
            if (HttpContext.User.Identity.Name != _dataService.GetProfile(dto.ProfileId).Email) return Unauthorized();
            
            if (!_dataService.UpdateBookmark(dto.ProfileId, dto.BookmarkId, dto.Note)) return NotFound();

            return Ok(); 
        }
        
        [Authorize]
        [HttpDelete]
        public IActionResult DeleteBookmark([FromBody] DeleteBookmarkDto dto)
        {
            if (HttpContext.User.Identity.Name != _dataService.GetProfile(dto.ProfileId).Email) return Unauthorized();
            
            if (!_dataService.DeleteBookmark(dto.ProfileId, dto.BookmarkId)) return NotFound();

            return Ok(); 
        }
    }
}