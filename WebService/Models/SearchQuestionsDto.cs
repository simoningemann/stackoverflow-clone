namespace WebService.Models
{
    public class SearchQuestionsDto
    {
        public int PageNum { get; set; }
        public int PageSize { get; set; }
        public string[] Keywords { get; set; }
    }
}