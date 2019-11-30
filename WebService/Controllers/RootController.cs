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
            var postId = 19;
            var keywords = new [] {"noob", "new", "post"};
            var welcome = "Welcome to the raw11 project portfolio rest api!";
            var searchQuestionsExample = Url.Link(nameof(QuestionsController.SearchQuestions), new {PageNum = 1, PageSize = 10, keywords});
            var getQuestionExample = Url.Link(nameof(QuestionsController.GetQuestion), new {postId});
            var getPostExample = Url.Link(nameof(PostsController.GetPost), new {postId});
            var getAnswersByAnswersToIdExample = Url.Link(nameof(AnswersController.GetAnswersByAnswerToId), new {postId});
            return Ok(new
            {
                welcome,
                searchQuestionsExample,
                getQuestionExample,
                getPostExample,
                getAnswersByAnswersToIdExample
            });
        }
    }
}