using System.Collections.Generic;
using rawdata_portfolioproject_2.Models;

namespace rawdata_portfolioproject_2.Services
{
    public interface IPostService
    {
        Post GetPost(int postId);
        List<Post> GetPosts(int[] postIds);
    }
}