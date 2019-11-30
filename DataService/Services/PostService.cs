using System.Collections.Generic;
using System.Linq;
using rawdata_portfolioproject_2.Models;

namespace rawdata_portfolioproject_2.Services
{
    public class PostService : IPostService
    {
        public Post GetPost(int postId)
        {
            using var db = new StackOverflowContext();
            var post = db.Posts.Find(postId);
            
            return post;
        }

        public List<Post> GetPosts(int[] postIds)
        {
            using var db = new StackOverflowContext();
            var posts = db.Posts.Where(x => postIds.Contains(x.PostId)).Select(x => x).ToList();

            return posts;
        }
    }
}