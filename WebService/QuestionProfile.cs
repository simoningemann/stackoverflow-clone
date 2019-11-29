using rawdata_portfolioproject_2;
using rawdata_portfolioproject_2.Models;
using Profile = AutoMapper.Profile;

namespace WebService
{
    public class QuestionProfile : Profile
    {
        public QuestionProfile()
        {
            CreateMap<Question, QuestionDto>();
        }
    }
}