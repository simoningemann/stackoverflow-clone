using System.Collections.Generic;
using rawdata_portfolioproject_2.Models;

namespace rawdata_portfolioproject_2.Services
{
    public interface IQuestionService
    {
        List<Question> SearchQuestions(string[] keywords);
    }
}