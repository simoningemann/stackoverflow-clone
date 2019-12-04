using rawdata_portfolioproject_2.Models;

namespace rawdata_portfolioproject_2.Services.Interfaces
{
    public interface IProfileService
    {
        Profile CreateProfile(string email, string salt, string hash);
        Profile GetProfile(string email);
        Profile DeleteProfile(string email);
    }
}