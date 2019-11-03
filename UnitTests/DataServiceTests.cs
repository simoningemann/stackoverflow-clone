using System;
using System.Linq;
using rawdata_portfolioproject_2;
using Xunit;

namespace UnitTests
{
    public class DataServiceTests
    {
        // add functions to test DataService like so:
        [Fact]
        public void DummyTest_ProfileClassExists()
        {
            Assert.NotNull(new Profile());
        }
        [Fact]
        public void GetPostWithValidId()
        {
            var service = new DataService();
            var post = service.GetPost(19);
            Assert.Equal(164, post.Score);
        }
    }
}