using LeoCore.Data.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
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

        public List<SelectListItem> GetLISTITEMSLISTUserCodesForUserCodeGroup(string pUserCodeGroupID,
            bool pAddBlankCode = true,
            bool pIncludeSoftDeleted = true);

        public USERCODE GetUserCode(
            string pUserCodeID,
            string pUserCodeGroupID);

        public string GetUserCodeDescription(
            string pUserCodeID,
            string pUserCodeGroupID);



    }
}
