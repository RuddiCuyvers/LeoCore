using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WGK.LMS.Business.Dtos.Trainings;
using WGK.LMS.Business.Dtos.IBF;

namespace WGK.LMS.Business.Models.IBF
{
    public class IBFIdentificationModel
    {
        #region Properties
       public IBFSearchCriteria SearchCriteria { get; set; }

        public ICollection<IBFInfo> MijnIBFTrainingen { get; set; }
        #endregion
    }
}
