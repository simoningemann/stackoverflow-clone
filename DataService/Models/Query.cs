using System;
using System.ComponentModel.DataAnnotations;

namespace rawdata_portfolioproject_2.Models
{
    public class Query
    {
        [Key]
        public int ProfileId { get; set; }
        public DateTime TimeSearched { get; set; } // what datatype to use here?? timestamp??
        public string QueryText { get; set; } // what datatype to use?? Query??
    }
}