using Microsoft.AspNetCore.Mvc;

namespace WebService.Controllers
{
    [ApiController]
    [Route("api")]
    public class RootController : ControllerBase
    {
        [HttpGet]
        public ActionResult GetRoot()
        {
            var welcome = "Welcome to the raw11 project portfolio rest api!";
            var searchQuestionsExample = "/api/questions?PageNum=2&PageSize=10&Keywords=noob&Keywords=new&Keywords=post";
            var getQuestionExample = "/api/questions/12473210";
            return Ok(new
            {
                welcome,
                searchQuestionsExample,
                getQuestionExample
            });
        }
    }
}