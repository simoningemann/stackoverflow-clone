using System.ComponentModel.DataAnnotations;

namespace rawdata_portfolioproject_2.Models
{
    public class Profile
    {
        [Key]
        public int ProfileId { get; set; }
        public string Email { get; set; }
        public string Salt { get; set; }
        public string Hash { get; set; }
    }
}