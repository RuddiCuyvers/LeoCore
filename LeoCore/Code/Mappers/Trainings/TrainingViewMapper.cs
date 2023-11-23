using LeoCore.Models.IBF;
using AutoMapper;
using LeoCore.Models.Trainings;
using LeoCore.Data;
using LEO.Common.Codes;
using System.Runtime.CompilerServices;

namespace LEO.Web.Code.Mappers.Trainings
{
    public class TrainingViewMapper : Profile
    {
        public TrainingViewMapper()
        {
            this.CreateMap<LeoCore.Data.Models.TRAINING, TrainingInfoViewModel>()
                .ForMember(dest => dest.TrainingID, opt => opt.MapFrom(src => src.ID))
                .ForMember(dest => dest.ONDERWERP, opt => opt.MapFrom(src => src.SUBJECT))
                .ForMember(dest => dest.TRAINING_TYPE, opt => opt.MapFrom(src => src.TRAINING_TYPE))    
                .ForMember(dest => dest.NOMENCLATUUR_YN, opt => opt.MapFrom(src => src.NOMENCL_CONV_YN))
                .ForMember(dest => dest.LINK, opt => opt.MapFrom(src => src.LINK ?? ""))
                .ForMember(dest => dest.INTERNEXTERN, opt => opt.MapFrom(src => src.TRAINER_INT_EXT));
        }
     }
}