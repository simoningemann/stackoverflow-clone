using System;

namespace WebService.Models
{
    public class PostDto
    {
        public string Link { get; set; }
        public int PostId { get; set; }
        public DateTime CreationDate { get; set; }
        public int Score { get; set; }
        public string Body { get; set; }
        public int UserId { get; set; }
    }
}