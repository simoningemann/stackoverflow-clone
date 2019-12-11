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
            var token = "eyJhbGciOiJIUzI1NiIsInR5pHQ0";
            var bookmarkId = 1;
            var note = "noteexample";
            var queryId = 1;

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
            
            var bookmarkForCreationExample = new BookmarkForCreation();
            bookmarkForCreationExample.PostId = postId;
            bookmarkForCreationExample.Note = note;
            
            var updateBookmarkDtoExample = new UpdateBookmarkDto();
            updateBookmarkDtoExample.BookmarkId = bookmarkId;
            updateBookmarkDtoExample.Note = note;
            
            var queryForCreationExample = new QueryForCreation();
            queryForCreationExample.QueryText = "noob new post";

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
            
            var getQuestionsExample = new
            {
                type = "GET",
                url = Url.Link(nameof(QuestionsController.GetQuestions), new {postIds})
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
                token,
                body = profileForDeletionExample
            };

            var updateProfilePasswordExample = new
            {
                type = "PUT",
                url = Url.Link(nameof(ProfilesController.UpdateProfilePassword), new {}),
                token,
                body = passwordForUpdateExample
            };

            var updateProfileEmailExample = new
            {
                type = "PUT",
                url = Url.Link(nameof(ProfilesController.UpdateProfileEmail), new { }),
                token,
                body = emailForUpdateExample
            };

            var getBookmarksExample = new
            {
                type = "GET",
                url = Url.Link(nameof(BookmarksController.GetBookmarks), new {}),
                token
            };

            var getBookmarkExample = new
            {
                type = "GET",
                url = Url.Link(nameof(BookmarksController.GetBookmark), new {bookmarkId}),
                token
            };

            var createBookmarkExample = new
            {
                type = "POST",
                url = Url.Link(nameof(BookmarksController.CreateBookmark), new {}),
                token,
                body = bookmarkForCreationExample
            };

            var updateBookmarkExample = new
            {
                type = "PUT",
                url = Url.Link(nameof(BookmarksController.UpdateBookmark), new {}),
                token,
                body = updateBookmarkDtoExample
            };

            var deleteBookmarkExample = new
            {
                type = "DELETE",
                url = Url.Link(nameof(BookmarksController.DeleteBookmark), new {bookmarkId}),
                token
            };

            var getQueriesExample = new
            {
                type = "GET",
                url = Url.Link(nameof(QueriesController.GetQueries), new {}),
                token
            };

            var createQueryExample = new
            {
                type = "POST",
                url = Url.Link(nameof(QueriesController.CreateQuery), new {}),
                body = queryForCreationExample,
                token
            };

            var deleteQueryExample = new
            {
                type = "DELETE",
                url = Url.Link(nameof(QueriesController.DeleteQuery), new {queryId}),
                token
            };

            return Ok(new
            {
                welcome,
                searchQuestionsExample,
                getQuestionExample,
                getQuestionsExample,
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
                updateProfileEmailExample,
                getBookmarksExample,
                getBookmarkExample,
                createBookmarkExample,
                updateBookmarkExample,
                deleteBookmarkExample,
                getQueriesExample,
                createQueryExample,
                deleteQueryExample
            });
        }
    }
}