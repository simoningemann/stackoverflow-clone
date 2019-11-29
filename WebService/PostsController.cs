using WebService;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Configuration;
using rawdata_portfolioproject_2;
using AutoMapper;
using rawdata_portfolioproject_2.Models;
using rawdata_portfolioproject_2.Services;

//namespace WebService.Controllers
namespace WebService
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

        [HttpGet("rankedsearch", Name = nameof(RankedSearch))] // works.. tested in postman
        public ActionResult RankedSearch([FromQuery] RankedSearchDto dto)
        {
            var result = _dataService.RankedWeightSearch(dto.PageSize, dto.PageNum, dto.Keywords);
            var questions = result.Item1;
            var totalQuestions = result.Item2; 
            
            var resultDto = new RankedSearchResultDto();
            resultDto.TotalQuestions = totalQuestions;//small hack
            resultDto.TotalPages = (int)Math.Ceiling((double)resultDto.TotalQuestions/dto.PageSize);
            var previousPage = dto.PageNum - 1;
            resultDto.PreviousPage = dto.PageNum -1 > 0
                ? Url.Link(nameof(RankedSearch), new {PageNum = previousPage,  dto.PageSize, dto.Keywords})
                : null;
            var nextPage = dto.PageNum + 1;
            resultDto.NextPage = dto.PageNum < resultDto.TotalPages
                ? Url.Link(nameof(RankedSearch), new {PageNum = nextPage, dto.PageSize, dto.Keywords})
                : null;
            
            var questionDtos = new List<QuestionDto>();

            foreach (var question in questions)
            {
                var questionDto = new QuestionDto();
                questionDto.Link = Url.Link(nameof(GetPost), new {postId = question.PostId});
                //questionDto.Question = question;
                questionDtos.Add(questionDto);
            }
            
            //resultDto.Questions = questionDtos;

            return Ok(resultDto);
        }

        //[HttpGet("{postId}", Name = nameof(GetPost))]
        //public ActionResult GetPost(int postId)
        //{
        //    var post = _dataService.GetPost(postId);
        //    if (post == null)
        //    {
        //        return NotFound();
        //    }
        //    return Ok(CreatePostDto(post));
        //}

        ///////////////////
        //
        // Helpers
        //
        //////////////////////

        /*
        private PostDto CreatePostDto(Post post)
        {
            var dto = new PostDto();
            dto.Body = post.Body;
            dto.Link = Url.Link(
                    nameof(RankedSearch),
                    new { postId = post.Id });
            return dto;
        }
        */

        
        private object CreateResult(IEnumerable<Ranked_Weight_VariadicResult> posts, PagingAttributes attr)
        {
            var totalItems = posts.Count();
            
            var numberOfPages = Math.Ceiling((double)totalItems / attr.PageSize);

            var prev = attr.Page > 0
                ? CreatePagingLink(attr.Page - 1, attr.PageSize)
                : null;
            var next = attr.Page < numberOfPages - 1
                ? CreatePagingLink(attr.Page + 1, attr.PageSize)
                : null;

            return new
            {
                totalItems,
                numberOfPages,
                prev,
                next,
                //items = posts.Select(CreatePostDto)
                items = posts
            };
        }

        private string CreatePagingLink(int page, int pageSize)
        {
            return Url.Link(nameof(RankedSearch), new { page, pageSize });
        }

    }
}
