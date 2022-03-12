using AutoMapper;
using NoLimitTech.Application.Models;
using NoLimitTech.Domain.Models;
using Payment.DataAccess.Enitities;
using System.Linq;

namespace NoLimitTech.WebApi.MapperProfiles
{
    public class SubscriptionPlanProfile : Profile
    {
        public SubscriptionPlanProfile()
        {
            CreateMap<SubscriptionPlan, SubscriptionPlanDTO>();
            CreateMap<SubscriptionFeature, SubscriptionFeatureDTO>()
                .ForMember(d => d.Name, o => o.MapFrom(s => s.Feature.Name));
        }
    }
}
