using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WGK.Lib.UseCases;
using WGK.LMS.Business.Models.IBF;


namespace WGK.LMS.Business.Interfaces.IBF
{
    public interface  IIBFIdentificationUseCase : IBaseUseCase
    {

        IBFIdentificationModel Result { get; }
    }
}
