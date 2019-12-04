using System;
using System.ComponentModel.DataAnnotations;

namespace rawdata_portfolioproject_2.Models
{
    public class Query
    {
        [Key]
        public int QueryId { get; set; }
        public int ProfileId { get; set; }
        public DateTime TimeSearched { get; set; }
        public string QueryText { get; set; }
    }
}