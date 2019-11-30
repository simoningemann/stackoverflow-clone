using System.Collections.Generic;
using System.Linq;
using rawdata_portfolioproject_2.Models;

namespace rawdata_portfolioproject_2.Services
{
    public class AnswerService : IAnswerService
    {
        public List<Answer> GetAnswersByAnswerToId(int postId)
        {
            using var db = new StackOverflowContext();
            var answers = db.Answers.Where(x => x.AnswerToId == postId).Select(x => x).ToList();

            return answers;
        }

        public Answer GetAnswer(int postId)
        {
            using var db = new StackOverflowContext();
            var answer = db.Answers.Find(postId);

            return answer;
        }
    }
}