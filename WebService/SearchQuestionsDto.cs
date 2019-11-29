namespace WebService
{
    public class SearchQuestionsDto
    {
        public int PageNum { get; set; }
        public int PageSize { get; set; }
        public string[] Keywords { get; set; }
    }
}