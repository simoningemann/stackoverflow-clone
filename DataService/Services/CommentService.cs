using System.Collections.Generic;
using System.Linq;
using rawdata_portfolioproject_2.Models;

namespace rawdata_portfolioproject_2.Services
{
    public class CommentService : ICommentService
    {
        public List<Comment> GetCommentsByPostIds(int[] postIds)
        {
            using var db = new StackOverflowContext();
            var comments = db.Comments.Where(x => postIds.Contains(x.PostId)).Select(x => x).ToList();

            return comments;
        }
    }
}