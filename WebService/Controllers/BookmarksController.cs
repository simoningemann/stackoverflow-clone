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
        private readonly IBookmarkService _bookmarkService;
        private readonly IConfiguration _configuration;
        //optional: add mapper

        public BookmarksController(IBookmarkService bookmarkService, IConfiguration configuration)
        {
            _bookmarkService = bookmarkService;
            _configuration = configuration;
            //optional add mapper
        }

        [Authorize]
        [HttpGet(Name = nameof(GetBookmarks))]
        public IActionResult GetBookmarks()
        {
            int.TryParse(HttpContext.User.Identity.Name, out var profileId);
            var bookmarks = _bookmarkService.GetAllBookmarks(profileId);

            return Ok(bookmarks);
            //var tags = new {tags = bookmarks};
            //return Ok(JsonConvert.SerializeObject(tags)); // is this correct?
        }
        
        [Authorize]
        [HttpPost]
        public IActionResult CreateBookmark([FromBody] CreateOrUpdateBookmarkDto dto)
        {
            int.TryParse(HttpContext.User.Identity.Name, out var profileId);

            var bookmark = _bookmarkService.CreateBookmark(profileId,
                dto.BookmarkId,
                dto.Note);
            
            if (bookmark == null) return BadRequest();

            return Ok(bookmark); // is return type ok?
        }
        
        [Authorize]
        [HttpPut]
        public IActionResult UpdateBookmark([FromBody] CreateOrUpdateBookmarkDto dto)
        {
            if (HttpContext.User.Identity.Name != _bookmarkService.GetProfile(dto.ProfileId).Email) return Unauthorized();
            
            if (!_bookmarkService.UpdateBookmark(dto.ProfileId, dto.BookmarkId, dto.Note)) return NotFound();

            return Ok(); 
        }
        
        [Authorize]
        [HttpDelete]
        public IActionResult DeleteBookmark([FromBody] DeleteBookmarkDto dto)
        {
            if (HttpContext.User.Identity.Name != _bookmarkService.GetProfile(dto.ProfileId).Email) return Unauthorized();
            
            if (!_bookmarkService.DeleteBookmark(dto.ProfileId, dto.BookmarkId)) return NotFound();

            return Ok(); 
        }
    }
}