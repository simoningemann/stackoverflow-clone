namespace WebService.Models
{
    public class CreateOrUpdateBookmarkDto
    {
        public int ProfileId { get; set; }
        public int BookmarkId { get; set; }
        public string Note { get; set; }
    }
}