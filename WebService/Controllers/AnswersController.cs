using System;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using rawdata_portfolioproject_2.Models;
using rawdata_portfolioproject_2.Services;
using rawdata_portfolioproject_2.Services.Interfaces;
using WebService.Controllers;

namespace WebService.Controllers
{
    [ApiController]
    [Route("api/answers")]
    public class AnswersController : ControllerBase
    {
        private readonly IAnswerService _answerService;
        private readonly IMapper _mapper;

        public AnswersController(IAnswerService answerService, IMapper mapper)
        {
            _answerService = answerService;
            _mapper = mapper;
        }

        [HttpGet("{postId}",Name = nameof(GetAnswersByAnswerToId))]
        public ActionResult GetAnswersByAnswerToId(int postId)
        {
            var thisLink = Url.Link(nameof(GetAnswersByAnswerToId), new {postId});
            var linkToQuestion = Url.Link(nameof(QuestionsController.GetQuestion), new {postId});
            var answers = _answerService.GetAnswersByAnswerToId(postId);
            
            return Ok(new
            {
                thisLink,
                linkToQuestion,
                answers
            });
        }
    }
}