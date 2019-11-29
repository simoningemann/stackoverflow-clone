using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using rawdata_portfolioproject_2.Models;
using rawdata_portfolioproject_2.Services;

//namespace WebService.Controllers
namespace WebService.Controllers
{
    [ApiController]
    [Route("api/posts")]
    public class PostsController : ControllerBase
    {
        private readonly IDataService _dataService;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public PostsController(IDataService dataService, IConfiguration configuration, IMapper mapper)
        {
            _dataService = dataService;
            _configuration = configuration;
            _mapper = mapper;
        }
        
        [HttpGet("{postId}", Name = nameof(GetPost))]
        public IActionResult GetPost(int postId)
        {
            var post = _dataService.GetPost(postId);

            if (post == null) return NotFound();

            return Ok(post);
        }
    }
}
