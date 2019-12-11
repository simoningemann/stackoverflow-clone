using System.Collections.Generic;
using rawdata_portfolioproject_2.Models;

namespace rawdata_portfolioproject_2.Services.Interfaces
{
    public interface IQuestionService
    {
        List<Question> SearchQuestions(string[] keywords);
        Question GetQuestion(int postId);
        List<Question> GetLinkedQuestions(int postId);
        List<Question> GetQuestions(int[] postIds);
    }
}