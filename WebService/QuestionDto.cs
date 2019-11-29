namespace WebService
{
    public class QuestionDto
    {
        public string Link { get; set; }
        public int PostId { get; set; }
        public string Title { get; set; }
        public string Tags { get; set; }
        public int? AcceptedAnswerId { get; set; }
    }
}