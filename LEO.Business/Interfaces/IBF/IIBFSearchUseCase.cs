using System;
using System.Collections.Generic;
using LEO.Business.Dtos.Trainings;

using LEO.Business.Dtos.IBF;

namespace LEO.Business.Interfaces.IBF
{
    public interface IIBFSearchUseCase
    {
        IBFSearchCriteria SearchCriteria { get; set; }
        ICollection<IBFInfo> Result { get; }
    }
}
