using System.Collections.Generic;
using System.Linq;
using rawdata_portfolioproject_2.Models;

namespace rawdata_portfolioproject_2.Services
{
    public class CommentService : ICommentService
    {
        public List<List<Comment>> GetCommentsByPostIds(int[] postIds)
        {
            using var db = new StackOverflowContext();
            var unorderedComments = db.Comments.Where(x => postIds.Contains(x.PostId)).Select(x => x).ToList();
            
            var orderedComments = new List<List<Comment>>();

            foreach (var postId in postIds)
            {
                var comments = unorderedComments.Where(x => x.PostId == postId).Select(x => x).ToList();
                
                orderedComments.Add(comments);
            }

            return orderedComments;
        }
    }
}