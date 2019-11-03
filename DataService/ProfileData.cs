using System;

namespace rawdata_portfolioproject_2
{
    // IMPORTANT CHOOSE WHICH ATTRIBUTES TO INCLUDE. HERE ARE SOME POTENTIAL ONES
    public class Profile
    {
        public int Id { get; set; }
        public  string Email { get; set; }
        // salt only needed in database
        // hash only needed in database
    }
    
    public class Query
    {
        public int ProfileId { get; set; }
        public DateTime TimeSearched { get; set; } // what datatype to use here?? timestamp??
        public string QueryText { get; set; } // what datatype to use?? Query??
    }
    
    public class Bookmark
    {
        public int ProfileId { get; set; }
        public int PostId { get; set; }
        public string Note { get; set; }
    } 
}