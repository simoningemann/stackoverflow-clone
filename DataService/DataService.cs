using System.Collections.Generic;
using System.Linq;


namespace rawdata_portfolioproject_2
{
    public class DataService : IDataService
    {
        // make functions here
        public Post GetPost(int postId)
        {
            using var db = new StackOverflowContext();
            return db.Posts.Find(postId);
        }
    }
}