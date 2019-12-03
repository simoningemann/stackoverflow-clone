using System.Collections.Generic;
using rawdata_portfolioproject_2.Models;

namespace rawdata_portfolioproject_2.Services.Interfaces
{
    public interface IUserService
    {
        User GetUser(int userId);
        List<User> GetUsers(int[] userIds);
    }
}