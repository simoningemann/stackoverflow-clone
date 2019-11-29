namespace rawdata_portfolioproject_2.Models
{
    public class RankedSearchResultDto
    {
        public int TotalQuestions { get; set; }
        public int TotalPages { get; set; }
        public string NextPage { get; set; }
        public string PreviousPage { get; set; }
        //public List<QuestionDto> Questions { get; set; }
    }
}