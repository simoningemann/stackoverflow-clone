using System.Linq;
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
        public ActionResult GetBookmarks()
        {
            int.TryParse(HttpContext.User.Identity.Name, out var profileId);
            var tempBookmarks = _bookmarkService.GetAllBookmarks(profileId);
            var bookmarks = tempBookmarks.Select(CreateBookmarkDto);

            return Ok(new
            {
                link = Url.Link(nameof(GetBookmarks), new {}),
                bookmarks
            });
            //var tags = new {tags = bookmarks};
            //return Ok(JsonConvert.SerializeObject(tags)); // is this correct?
        }
        
        [Authorize]
        [HttpGet("{bookmarkId}",Name = nameof(GetBookmark))]
        public ActionResult GetBookmark(int bookmarkId)
        {
            int.TryParse(HttpContext.User.Identity.Name, out var profileId);
            var bookmark = _bookmarkService.GetBookmark(bookmarkId);

            if (bookmark == null) return NotFound();
            if (bookmark.ProfileId != profileId) return Unauthorized("Bookmark does not belong to profile");

            return Ok(CreateBookmarkDto(bookmark));
            //var tags = new {tags = bookmarks};
            //return Ok(JsonConvert.SerializeObject(tags)); // is this correct?
        }
        
        [Authorize]
        [HttpPost(Name = nameof(CreateBookmark))]
        public ActionResult CreateBookmark([FromBody] BookmarkForCreation forCreation)
        {
            int.TryParse(HttpContext.User.Identity.Name, out var profileId);

            var bookmark = _bookmarkService.CreateBookmark(profileId,
                forCreation.PostId,
                forCreation.Note);
            
            if (bookmark == null) return BadRequest();

            return Created("", CreateBookmarkDto(bookmark));
        }
        
        [Authorize]
        [HttpPut]
        public ActionResult UpdateBookmark([FromBody] UpdateBookmarkDto dto)
        {
            int.TryParse(HttpContext.User.Identity.Name, out var profileId);
            var bookmark = _bookmarkService.UpdateBookmark(dto.BookmarkId, profileId, dto.Note);

            if (bookmark == null) return BadRequest("Error updating bookmark, bookmark not found or bookmark does not belong to profile.");

            return Ok(CreateBookmarkDto(bookmark)); 
        }
        
        [Authorize]
        [HttpDelete]
        public ActionResult DeleteBookmark(int bookmarkId)
        {
            int.TryParse(HttpContext.User.Identity.Name, out var profileId);
            
            var bookmark = _bookmarkService.DeleteBookmark(bookmarkId, profileId);

            if (bookmark == null) return BadRequest();

            return Ok("Deleted bookmark: " + bookmark.BookmarkId); 
        }

        private BookmarkDto CreateBookmarkDto(Bookmark bookmark)
        {
            var dto = new BookmarkDto();
            dto.Link = Url.Link(nameof(GetBookmark), new {bookmark.BookmarkId});
            dto.BookmarkId = bookmark.BookmarkId;
            dto.PostLink = Url.Link(nameof(PostsController.GetPost), new {bookmark.PostId});
            dto.PostId = bookmark.PostId;
            dto.Note = bookmark.Note;

            return dto;
        }
    }
}