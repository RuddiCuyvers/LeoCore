using LeoCore.Data.Models;
using System;
using System.Collections.Generic;

using System.Linq;

namespace LeoCore.Data
{
    public interface IUserCodeRepository
    {
      

            
        public List<LeoCore.Data.Models.USERCODE> GetUserCodesForUserCodeGroup(
            string pUserCodeGroupID,
            bool pAddBlankCode = true,
            bool pIncludeSoftDeleted = true,
            string pParentUserCodeID = null,
            string pDependencyCode = null,
            IEnumerable<string> pUserCodeIDs = null);

       

       

      
       

       

       

    }
}
