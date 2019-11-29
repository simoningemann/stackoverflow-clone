using rawdata_portfolioproject_2.Models;
using WebService.Models;
using Profile = AutoMapper.Profile;

namespace WebService.Profiles
{
    public class QuestionProfile : Profile
    {
        public QuestionProfile()
        {
            CreateMap<Question, QuestionDto>();
        }
    }
}