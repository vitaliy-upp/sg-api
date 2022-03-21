using AutoMapper;
using Microsoft.AspNetCore.Http;
using Domain.Repository.Interfaces.KidProfile;
using Domain.DataAccess.Entities.KidProfile;

namespace Domain.BusinessLogic.Services
{
    public class PersonalityQuestionService : BusinessService<PersonalityQuestion, int, PersonalityQuestion> {
        public PersonalityQuestionService(IHttpContextAccessor httpContextAccessor
            , IPersonalityQuestionRepository repository
            , IMapper mapper
            ) : base(httpContextAccessor
                , repository
                , mapper)
        {
        }

    }
}
