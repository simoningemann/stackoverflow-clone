using System.ComponentModel.DataAnnotations;

namespace rawdata_portfolioproject_2.Models
{
    public class Answer
    {
        [Key]
        public int PostId { get; set; }
        public int AnswerToId { get; set; }
    }
}