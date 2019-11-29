namespace WebService.Models
{
    public class RankedSearchDto
    {
        public int PageNum { get; set; }
        public int PageSize { get; set; }
        public string[] Keywords { get; set; }
    }
}