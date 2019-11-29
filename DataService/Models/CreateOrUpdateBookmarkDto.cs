namespace rawdata_portfolioproject_2.Models
{
    public class CreateOrUpdateBookmarkDto
    {
        public int ProfileId { get; set; }
        public int BookmarkId { get; set; }
        public string Note { get; set; }
    }
}