using System;
using System.ComponentModel.DataAnnotations;

namespace rawdata_portfolioproject_2.Models
{
    public class Post
    {
        [Key]
        public int PostId { get; set; }
        public DateTime CreationDate { get; set; }
        public int Score { get; set; }
        public string Body { get; set; }
        public int UserId { get; set; }
    }
}