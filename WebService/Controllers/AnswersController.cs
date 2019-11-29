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
    [Route("api/Answers")]
    public class AnswersController : ControllerBase
    {
        private readonly IQuestionService _questionService;
        private readonly IMapper _mapper;

        public AnswersController(IQuestionService questionService, IMapper mapper)
        {
            _questionService = questionService;
            _mapper = mapper;
        }
    }
}