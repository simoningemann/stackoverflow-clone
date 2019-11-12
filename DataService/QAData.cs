using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace rawdata_portfolioproject_2
{
    // IMPORTANT CHOOSE WHICH ATTRIBUTES TO INCLUDE: HERE ARE SOME POTENTIAL ONES
    public class Post
    {
        public int Id { get; set; }
        public DateTime CreationDate { get; set; }
        public int Score { get; set; }
        public string Body { get; set; }
        public int UserId { get; set; }
        public Question Question { get; set; }
        public Answer Answer { get; set; }
        public User User { get; set; }
    }
    
    public class Question
    {
        [Key]
        public int PostId { get; set; }
        public string Title { get; set; }
        public string Tags { get; set; }
        public int? AcceptedAnswerId { get; set; }
    }
    
    public class Link
    {
        public int PostId { get; set; }
        public int LinkToPostId { get; set; }
    }
    
    public class Answer
    {
        [Key]
        public int PostId { get; set; }
        public int AnswerToId { get; set; }
    }
    
    public class Comment
    {
        public int Id { get; set; }
        public int PostId { get; set; }
        public DateTime CreationDate { get; set; }
        public int Score { get; set; }
        public string Body { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
    }
    
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CreationDate { get; set; }
        public string Location { get; set; }
        public int? Age { get; set; }
    }
    
    public class Word // check actual inverted index in db... is this needed in DAL?
    {
        
    }
}