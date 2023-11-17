using System;
using System.Collections.Generic;
using WGK.LMS.Business.Dtos.Trainings;
using WGK.Lib.UseCases;
using WGK.LMS.Business.Dtos.IBF;

namespace WGK.LMS.Business.Interfaces.IBF
{
    public interface IIBFSearchUseCase : IBaseUseCase
    {
        IBFSearchCriteria SearchCriteria { get; set; }
        ICollection<IBFInfo> Result { get; }
    }
}
