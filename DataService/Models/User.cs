using System;
using System.ComponentModel.DataAnnotations;

namespace rawdata_portfolioproject_2.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }
        public string Name { get; set; }
        public DateTime TimeSignedUp { get; set; }
        public string Location { get; set; }
        public int? Age { get; set; }
    }
}