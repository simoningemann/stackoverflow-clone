using System;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using rawdata_portfolioproject_2.Models;
using rawdata_portfolioproject_2.Services;
using WebService.Models;

namespace WebService.Controllers
{
    [ApiController]
    [Route("api/questions")]
    public class QuestionController : ControllerBase
    {
        private readonly IQuestionService _questionService;
        private readonly IMapper _mapper;

        public QuestionController(IQuestionService questionService, IMapper mapper)
        {
            _questionService = questionService;
            _mapper = mapper;
        }
        
        [HttpGet(Name = nameof(SearchQuestions))] 
        public ActionResult SearchQuestions([FromQuery] SearchQuestionsDto dto)
        {
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
                totalQuestions,
                totalPages,
                prevPage,
                nextPage,
                questions
                
            });
        }
        
        [HttpGet("{questionId}", Name = nameof(GetQuestion))] 
        public ActionResult GetQuestion(int questionId)
        {
            return Ok();
        }

        private QuestionDto CreateQuestionDto(Question question)
        {
            var dto = _mapper.Map<QuestionDto>(question);
            dto.Link = Url.Link(
                nameof(GetQuestion),
                new { questionId = question.PostId });
            return dto;
        }
    }
}