using System;
using rawdata_portfolioproject_2;
using rawdata_portfolioproject_2.Models;
using rawdata_portfolioproject_2.Services;
using Xunit;

namespace UnitTests
{
    public class DataServiceTests
    {
        // add functions to test DataService like so:
        [Fact]
        public void GetPostWithValidId()
        {
            var service = new PostService();
            var post = service.GetPost(19);
            Assert.Equal(164, post.Score);
        }
    }
}