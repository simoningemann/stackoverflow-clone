namespace rawdata_portfolioproject_2.Models
{
    public class WordWeight
    {
        public int PostId { get; set; }
        public string Word { get; set; }
        public double Weight { get; set; }
    }

    public static class WordWeightExtentionMethods
    {
        public static WordWeight MultiplyWeight(this WordWeight wordWeight)
        {
            wordWeight.Weight = wordWeight.Weight * 100000;

            return wordWeight;
        }
    }
}