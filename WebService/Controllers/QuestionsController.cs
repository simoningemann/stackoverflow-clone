using System;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using rawdata_portfolioproject_2.Models;
using rawdata_portfolioproject_2.Services;
using rawdata_portfolioproject_2.Services.Interfaces;
using WebService.Models;

namespace WebService.Controllers
{
    [ApiController]
    [Route("api/questions")]
    public class QuestionsController : ControllerBase
    {
        private readonly IQuestionService _questionService;
        private readonly IMapper _mapper;

        public QuestionsController(IQuestionService questionService, IMapper mapper)
        {
            _questionService = questionService;
            _mapper = mapper;
        }
        
        [HttpGet(Name = nameof(SearchQuestions))] 
        public ActionResult SearchQuestions([FromQuery] SearchQuestionsDto dto)
        {
            var thisPage = Url.Link(nameof(SearchQuestions), new {dto.PageNum, dto.PageSize, dto.Keywords});
            var tempQuestions = _questionService.SearchQuestions(dto.Keywords);
            var totalQuestions = tempQuestions.Count;
            var totalPages = (int)Math.Ceiling((double)totalQuestions/dto.PageSize);
            
            var prevPage = dto.PageNum -1 > 0
                ? Url.Link(nameof(SearchQuestions), new {PageNum = (dto.PageNum - 1),  dto.PageSize, dto.Keywords})
                : null;
            
            var nextPage = dto.PageNum < totalPages
                ? Url.Link(nameof(SearchQuestions), new {PageNum = (dto.PageNum + 1), dto.PageSize, dto.Keywords})
                : null;
            
            tempQuestions = tempQuestions
                .Skip((dto.PageNum -1) * dto.PageSize)
                .Take(dto.PageSize)
                .ToList();

            var questions = tempQuestions.Select(CreateQuestionDto);
            
            return Ok(new
            {
                thisPage,
                dto.Keywords,
                dto.PageSize,
                totalQuestions,
                totalPages,
                prevPage,
                nextPage,
                questions
                
            });
        }
        
        [HttpGet("{postId}", Name = nameof(GetQuestion))] 
        public ActionResult GetQuestion(int postId)
        {
            var question = _questionService.GetQuestion(postId);

            if (question == null)
                return NotFound();
            
            return Ok(CreateQuestionDto(question));
        }
        
        [HttpGet("links/{postId}", Name = nameof(GetLinkedQuestions))]
        public ActionResult GetLinkedQuestions(int postId)
        {
            var thisLink = Url.Link(nameof(GetLinkedQuestions), new {postId});
            var questionLink = Url.Link(nameof(GetQuestion), new{ postId });
            var questions = _questionService.GetLinkedQuestions(postId).Select(CreateQuestionDto);
            
            return Ok(new
            {
                postId,
                thisLink,
                questionLink,
                questions
            });
        }
        private QuestionDto CreateQuestionDto(Question question)
        {
            var dto = _mapper.Map<QuestionDto>(question);
            dto.Link = Url.Link(
                nameof(GetQuestion),
                new { question.PostId });
            return dto;
        }
    }
}