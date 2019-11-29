using System;

namespace rawdata_portfolioproject_2.Models
{
    public class CreateQueryDto
    {
        public int ProfileId { get; set; }
        public DateTime TimeSearched { get; set; }
        public string QueryText { get; set; }
    }
}