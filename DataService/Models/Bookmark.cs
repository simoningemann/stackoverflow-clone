using System.ComponentModel.DataAnnotations;

namespace rawdata_portfolioproject_2.Models
{
    // IMPORTANT CHOOSE WHICH ATTRIBUTES TO INCLUDE. HERE ARE SOME POTENTIAL ONES

    public class Bookmark
    {
        [Key]
        public int ProfileId { get; set; }
        public int PostId { get; set; }
        public string Note { get; set; }
    }
}