using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LEO.Business.Dtos.Trainings;
using LEO.Business.Dtos.IBF;

namespace LEO.Business.Models.IBF
{
    public class IBFIdentificationModel
    {
        #region Properties
       public IBFSearchCriteria SearchCriteria { get; set; }

        public ICollection<IBFInfo> MijnIBFTrainingen { get; set; }
        #endregion
    }
}
