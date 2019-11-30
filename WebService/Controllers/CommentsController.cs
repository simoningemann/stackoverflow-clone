using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using rawdata_portfolioproject_2.Models;
using rawdata_portfolioproject_2.Services;
using WebService.Controllers;

namespace WebService.Controllers
{
    [ApiController]
    [Route("api/comments")]
    public class CommentsController : ControllerBase
    {
        private readonly ICommentService _commentService;
        private readonly IMapper _mapper;

        public CommentsController(ICommentService commentService, IMapper mapper)
        {
            _commentService = commentService;
            _mapper = mapper;
        }

        [HttpGet(Name = nameof(GetCommentsByPostIds))]
        public ActionResult GetCommentsByPostIds([FromQuery] int[] postIds)
        {
            var link = Url.Link(nameof(GetCommentsByPostIds), new {postIds});
            var getPostsLink = Url.Link(nameof(PostsController.GetPosts), new {postIds});

            var comments = _commentService.GetCommentsByPostIds(postIds);

            return Ok( new
                {
                    link,
                    postIds,
                    getPostsLink,
                    comments
                }
            );
        }
    }
}