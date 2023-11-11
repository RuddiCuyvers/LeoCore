using LeoCore.Models.IBF;
using AutoMapper;

namespace LEO.Web.Code.Mappers.IBF
{
    public class IBFViewMapper  : Profile
    {
        public IBFViewMapper()
        {
            this.CreateMap<LeoCore.Data.Models.TRAINING, IBFIdentification_DTO>()
                .ForMember(dest => dest.TrainingID, opt => opt.MapFrom(src => src.ID))
                .ForMember(dest => dest.Person_TrainingID, opt => opt.MapFrom(src => src.PERSON_TRAININGs.FirstOrDefault().ID))
                .ForMember(dest => dest.NOMENCLATUUR_YN, opt => opt.MapFrom(src => src.NOMENCL_CONV_YN))
                .ForMember(dest => dest.LINK, opt => opt.MapFrom(src => src.LINK ?? ""))
                .ForMember(dest => dest.DATUMTRAINING, opt => opt.MapFrom(src => ""));
        }
    }
}