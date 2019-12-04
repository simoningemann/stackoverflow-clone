using Microsoft.AspNetCore.Mvc;
using rawdata_portfolioproject_2.Models;
using WebService.Models;

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
            var postIds = new[] {19, 71, 531};
            var userId = 1;
            var userIds = new[] {1, 3, 4};
            var keywords = new[] {"noob", "new", "post"};
            var email = "example@email.com";
            var password = "examplepassword";

            var profileForCreationExample = new ProfileForCreation();
            profileForCreationExample.Email = email;
            profileForCreationExample.Password = password;

            var profileForLoginExample = new ProfileForLogin();
            profileForLoginExample.Email = email;
            profileForLoginExample.Password = password;
            
            var profileForDeletionExample = new ProfileForDeletion();
            profileForDeletionExample.Password = password;

            var passwordForUpdateExample = new PasswordForUpdate();
            passwordForUpdateExample.OldPassword = password;
            passwordForUpdateExample.NewPassword = password;
            
            var emailForUpdateExample = new EmailForUpdate();
            emailForUpdateExample.NewEmail = email;
            emailForUpdateExample.Password = password;

                // QA API
            var welcome = "Welcome to the raw11 project portfolio rest api!";

            var searchQuestionsExample = new
            {
                type = "GET",
                url = Url.Link(nameof(QuestionsController.SearchQuestions), new {PageNum = 1, PageSize = 10, keywords}),
            };

            var getQuestionExample = new
            {
                type = "GET",
                url = Url.Link(nameof(QuestionsController.GetQuestion), new {postId})
            };

            var getLinksExample = new
            {
                type = "GET", 
                url = Url.Link(nameof(QuestionsController.GetLinkedQuestions), new {postId})
            };
            
            var getPostExample = new
            {
                type = "GET",
                url = Url.Link(nameof(PostsController.GetPost), new {postId})
            };
            
            var getPostsExample = new
            {
                type = "GET",
                url = Url.Link(nameof(PostsController.GetPosts), new {postIds})
            };
            
            var getAnswersByAnswersToIdExample = new
            {
                type = "GET",
                url = Url.Link(nameof(AnswersController.GetAnswersByAnswerToId), new {postId})
            };
            
            var getCommentsByPostIdsExample = new
            {
                type = "GET",
                url = Url.Link(nameof(CommentsController.GetCommentsByPostIds), new {postIds})
            };
            
            var getWordCloudExample = new
            {
                type = "GET",
                url = Url.Link(nameof(PostsController.GetWordCloud), new {postId})
            };
            
            var getUserExample = new 
            {
                type = "GET",
                url = Url.Link(nameof(UsersController.GetUser), new{userId})
            };
            
            var getUsersExample = new {
                type = "GET",
                url = Url.Link(nameof(UsersController.GetUsers), new {userIds})
            };
           
            // Profile API
            var createProfileExample = new {
                type = "POST",
                url = Url.Link(nameof(ProfilesController.CreateProfile), new {}),
                body = profileForCreationExample
            };
            
            var profileLoginExample = new
            {
                type = "POST",
                url = Url.Link(nameof(ProfilesController.ProfileLogin), new {}),
                body = profileForLoginExample
            };
            
            var deleteProfileExample = new
            {
                type = "POST",
                url = Url.Link(nameof(ProfilesController.DeleteProfile), new {}),
                body = profileForDeletionExample
            };

            var updateProfilePasswordExample = new
            {
                type = "PUT",
                url = Url.Link(nameof(ProfilesController.UpdateProfilePassword), new {}),
                body = passwordForUpdateExample
            };

            var updateProfileEmailExample = new
            {
                type = "PUT",
                url = Url.Link(nameof(ProfilesController.UpdateProfileEmail), new { }),
                body = emailForUpdateExample
            };

            return Ok(new
            {
                welcome,
                searchQuestionsExample,
                getQuestionExample,
                getLinksExample,
                getPostExample,
                getPostsExample,
                getAnswersByAnswersToIdExample,
                getCommentsByPostIdsExample,
                getWordCloudExample,
                getUserExample,
                getUsersExample,
                createProfileExample,
                profileLoginExample,
                deleteProfileExample,
                updateProfilePasswordExample,
                updateProfileEmailExample
            });
        }
    }
}