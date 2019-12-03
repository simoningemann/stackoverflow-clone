namespace WebService
{
    public static class ExtentionMethods
    {
        public static int Clamp(this int val, int min, int max)
        {
            if (val < min) return min;
            if (max < val) return max;
            return val;
        }
    }
}