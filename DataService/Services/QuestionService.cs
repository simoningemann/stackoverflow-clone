using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using rawdata_portfolioproject_2.Models;

namespace rawdata_portfolioproject_2.Services
{
    public class QuestionService : IQuestionService
    {
        public List<Question> SearchQuestions(string[] keywords)
        {
            using var db = new StackOverflowContext();
            var query = CreateSearchQuestionsQuery(keywords);
            var questions = db.Questions.FromSqlRaw(query).ToList();
            
            return questions;
        }
        
        public Question GetQuestion (int postId)
        {
            using var db = new StackOverflowContext();
            var question = db.Questions.Find(postId);
            return question;
        }

        private string CreateSearchQuestionsQuery(string[] keywords)
        {
            var query = "select * from searchquestions(";
            foreach (var keyword in keywords)
            {
                query = query + "'" + keyword + "',";
            }

            query = query.Remove(query.Length-1); // remove the last comma
            query = query + ");";
            return query;
        }
    }
}