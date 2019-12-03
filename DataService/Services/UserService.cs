using System.Collections.Generic;
using System.Linq;
using rawdata_portfolioproject_2.Models;
using rawdata_portfolioproject_2.Services.Interfaces;

namespace rawdata_portfolioproject_2.Services
{
    public class UserService : IUserService
    {
        public User GetUser(int userId)
        {
            using var db = new StackOverflowContext();
            var user = db.Users.Find(userId);

            return user;
        }

        public List<User> GetUsers(int[] userIds)
        {
            using var db = new StackOverflowContext();
            var users = db.Users.Where(x => userIds.Contains(x.UserId)).Select(x => x).ToList();

            return users;
        }
    }
}