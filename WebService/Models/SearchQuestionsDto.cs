namespace WebService.Models
{
    public class SearchQuestionsDto
    {
        private const int _minPageSize = 10;
        private const int _maxPageSize = 50;
        private int _pageSize = 10;
        public int PageNum { get; set; }
        public int PageSize { get => _pageSize; set => _pageSize = value.Clamp(_minPageSize, _maxPageSize); }
        public string[] Keywords { get; set; }
    }
}