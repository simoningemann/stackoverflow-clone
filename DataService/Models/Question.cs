using System.ComponentModel.DataAnnotations;

namespace rawdata_portfolioproject_2.Models
{
    public class Question
    {
        [Key]
        public int PostId { get; set; }
        public string Title { get; set; }
        public string Tags { get; set; }
        public int? AcceptedAnswerId { get; set; }
    }
}