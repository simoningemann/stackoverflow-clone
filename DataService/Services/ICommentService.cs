using System.Collections.Generic;
using rawdata_portfolioproject_2.Models;

namespace rawdata_portfolioproject_2.Services
{
    public interface ICommentService
    {
        List<Comment> GetCommentsByPostIds(int[] postIds);
    }
}