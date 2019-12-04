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
        private readonly IQueryService _queryService;
        private readonly IConfiguration _configuration;
        //optional: add mapper

        public QueriesController(IQueryService queryService, IConfiguration configuration)
        {
            _queryService = queryService;
            _configuration = configuration;
            //optional add mapper
        }

        [Authorize]
        [HttpGet(Name = nameof(GetQueries))]
        public ActionResult GetQueries()
        {
            int.TryParse(HttpContext.User.Identity.Name, out var profileId);

            var queries = _queryService.GetQueries(profileId);

            return Ok(queries);
        }
        
        [Authorize]
        [HttpPost(Name = nameof(CreateQuery))]
        public ActionResult CreateQuery([FromBody] QueryForCreation dto)
        {
            int.TryParse(HttpContext.User.Identity.Name, out var profileId);

            var query = _queryService.CreateQuery(profileId, dto.QueryText);
            
            if (query == null) return BadRequest();

            return Created("", query);
        }

        [Authorize]
        [HttpDelete("{queryId}", Name = nameof(DeleteQuery))]
        public ActionResult DeleteQuery(int queryId)
        {
            int.TryParse(HttpContext.User.Identity.Name, out var profileId);

            var query = _queryService.DeleteQuery(queryId, profileId);
            
            if(query == null) return BadRequest();

            return Ok(query); 
        }
    }
}