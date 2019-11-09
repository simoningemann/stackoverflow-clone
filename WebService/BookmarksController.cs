using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using rawdata_portfolioproject_2;

namespace WebService // also add controllers for other ressources in the same way as below
{
    [ApiController]
    [Route("api/profiles")]
    public class BookmarksController : ControllerBase
    {
        private readonly IDataService _dataService;
        private readonly IConfiguration _configuration;
        //optional: add mapper

        public BookmarksController(IDataService dataService, IConfiguration configuration)
        {
            _dataService = dataService;
            _configuration = configuration;
            //optional add mapper
        }
        
    }
}