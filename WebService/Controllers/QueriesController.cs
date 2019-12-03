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
    [Route("api/queries")]
    public class QueriesController : ControllerBase
    {
        private readonly IDataService _dataService;
        private readonly IConfiguration _configuration;
        //optional: add mapper

        public QueriesController(IDataService dataService, IConfiguration configuration)
        {
            _dataService = dataService;
            _configuration = configuration;
            //optional add mapper
        }

        [Authorize]
        [HttpGet("{profileId}")]
        public IActionResult GetAllQueries(int profileId)
        {
            if (HttpContext.User.Identity.Name != _dataService.GetProfile(profileId).Email) return Unauthorized();

            var queries = _dataService.GetAllQueries(profileId);

            var tags = new {tags = queries};

            return Ok(JsonConvert.SerializeObject(tags)); // is this correct?
        }
        
        [Authorize]
        [HttpPost]
        public IActionResult CreateQuery([FromBody] CreateQueryDto dto)
        {
            if (HttpContext.User.Identity.Name != _dataService.GetProfile(dto.ProfileId).Email) return Unauthorized();

            var query = _dataService.CreateQuery(dto.ProfileId, dto.TimeSearched, dto.QueryText);
            
            if (query == null) return BadRequest();

            return Ok(query); // is return type ok?
        }

        [Authorize]
        [HttpDelete]
        public IActionResult DeleteQuery([FromBody] DeleteQueryDto dto)
        {
            if (HttpContext.User.Identity.Name != _dataService.GetProfile(dto.ProfileId).Email) return Unauthorized();
            
            if (!_dataService.DeleteQuery(dto.ProfileId, dto.TimeSearched)) return NotFound();

            return Ok(); 
        }
        
        [Authorize]
        [HttpDelete("{profileId}")]
        public IActionResult DeleteAllQueries(int profileId)
        {
            if (HttpContext.User.Identity.Name != _dataService.GetProfile(profileId).Email) return Unauthorized();

            _dataService.DeleteQueries(_dataService.GetAllQueries(profileId));

            return Ok(); 
        }
    }
}