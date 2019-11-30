using System.Collections.Generic;
using rawdata_portfolioproject_2.Models;

namespace rawdata_portfolioproject_2.Services
{
    public interface IAnswerService
    {
        List<Answer> GetAnswersByAnswerToId(int postId);
        Answer GetAnswer(int postId);
    }
}