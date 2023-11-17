using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using LEO.Business.Models.IBF;


namespace LEO.Business.Interfaces.IBF
{
    public interface  IIBFIdentificationUseCase 
    {

        IBFIdentificationModel Result { get; }
    }
}
