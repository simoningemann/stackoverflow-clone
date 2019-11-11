using System;
using rawdata_portfolioproject_2;

namespace SimpleTests
{
    class Program
    {
        static void Main(string[] args)
        {
            var service = new DataService();
            
            var keywords = new string[] {"noob", "new", "post"};
            Console.WriteLine(service.CreateSearchQuery(keywords));
        }
    }
}