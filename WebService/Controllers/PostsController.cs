using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using rawdata_portfolioproject_2.Models;
using rawdata_portfolioproject_2.Services;
using WebService.Models;

//namespace WebService.Controllers
namespace WebService.Controllers
{
    [ApiController]
    [Route("api/posts")]
    public class PostsController : ControllerBase
    {
        private readonly IPostService _postService;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public PostsController(IPostService postService, IConfiguration configuration, IMapper mapper)
        {
            _postService = postService;
            _configuration = configuration;
            _mapper = mapper;
        }
        
        [HttpGet("{postId}", Name = nameof(GetPost))]
        public ActionResult GetPost(int postId)
        {
            var post = _postService.GetPost(postId);

            if (post == null) return NotFound();

            return Ok(CreatePostDto(post));
        }

        [HttpGet(Name = nameof(GetPosts))]
        public ActionResult GetPosts([FromQuery] int [] postIds)
        {
            var link = Url.Link(nameof(GetPosts), new {postIds});
            var tempPosts = _postService.GetPosts(postIds);

            var posts = tempPosts.Select(CreatePostDto);

            return Ok(new
            {
                link,
                posts
            });
        }

        private PostDto CreatePostDto(Post post)
        {
            var dto = _mapper.Map<PostDto>(post);
            dto.Link = Url.Link(
                nameof(GetPost),
                new { postId = post.PostId });
            return dto;
        }
    }
}
