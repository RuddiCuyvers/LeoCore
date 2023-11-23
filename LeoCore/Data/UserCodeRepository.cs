using LeoCore.Data.Models;
using System;
using System.Collections.Generic;

using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;
using LEO.Common.Codes;
using Microsoft.IdentityModel.Tokens;

namespace LeoCore.Data
{
    public class UserCodeRepository : IUserCodeRepository
    {
        #region Constants
        // Special case for the 'UserCode' table itself
        private const string cSqlCommandDefaultUserCodeTable =
            "SELECT UserCodeID, UserCodeGroupID, ParentUserCodeID, ParentUserCodeGroupID, Description, DependencySet, SortOrder, SoftDeleted, NULL AS Remark, NULL AS RowVersion FROM UserCode";
        //UserCodeGroup
        private const string cSqlCommandDefaultUserCodeGroupTable =
           "SELECT UserCodeGroupID AS UserCodeID , null AS UserCodeGroupID, null AS ParentUserCodeID, null AS ParentUserCodeGroupID, Description, DependencySet, SortOrder, SoftDeleted, NULL AS Remark, NULL AS RowVersion FROM UserCode";
        // Standard user code tables (= 'green' tables)
        private const string cSqlCommandOtherUserCodeTable =
             "SELECT CAST(ID AS VARCHAR2(20)) AS UserCodeID, {1}  AS UserCodeGroupID, null AS ParentUserCodeID, {3} AS ParentUserCodeGroupID, {4} AS Description, NULL AS DependencySet, NULL As SortOrder, 'N' AS SoftDeleted, NULL AS Remark, NULL AS RowVersion FROM {0}";
        #endregion

        #region Fields


        #endregion

        #region Constructors
        private readonly LeoDBContext _context;
        #region Constructors
        public UserCodeRepository(LeoDBContext context)
        {
            _context = context;
        }
        #endregion
        #endregion

        #region Private Properties
        private USERCODE BlankUserCode
        {
            get
            {
                return new LeoCore.Data.Models.USERCODE
                {
                    USERCODEID = @"_BLNK",
                    DESCRIPTION = ""
                };
            }
        }
        #endregion


        public List<LeoCore.Data.Models.USERCODE> GetUserCodesForUserCodeGroup(
            string pUserCodeGroupID,
            bool pAddBlankCode = true,
            bool pIncludeSoftDeleted = true,
            string pParentUserCodeID = null,
            string pDependencyCode = null,
            IEnumerable<string> pUserCodeIDs = null)
        {
            if (String.IsNullOrEmpty(pUserCodeGroupID))
            {
                return null;
            }
            var vResult = new List<LeoCore.Data.Models.USERCODE>();
            if (pAddBlankCode)
            {
                vResult.Add(BlankUserCode);
            }

            IEnumerable<LeoCore.Data.Models.USERCODE> vQuery = _context.USERCODE
                .Where(p => p.USERCODEGROUPID == pUserCodeGroupID);

            if (!String.IsNullOrEmpty(pParentUserCodeID))
            {
                // Filter UserCodes that have the specified parent UserCode
                vQuery = vQuery
                    .Where(p => p.PARENTUSERCODEID == pParentUserCodeID);
            }

            if (!pIncludeSoftDeleted)
            {
                // Filter UserCodes that are not soft deleted
                vQuery = vQuery
                    .Where(p => (p.SOFTDELETED == "0"));
            }

            if (!String.IsNullOrEmpty(pDependencyCode))
            {
                // Filter UserCodes that have the specified dependency UserCode in their DependencyCodeBag
                vQuery = vQuery
                    .Where(p => (p.DEPENDENCYSET != null) && p.DEPENDENCYSET.Contains(pDependencyCode));
            }

            if (pUserCodeIDs != null && pUserCodeIDs.Any())
            {
                // Fetch UserCodes for the specified UserCodeIDs
                vQuery = vQuery
                    .Where(p => pUserCodeIDs.Contains(p.USERCODEID));
            }

            vResult.AddRange(vQuery
                .OrderBy(p => p.SORTORDER)
                .ThenBy(p => p.DESCRIPTION));

           
            return vResult;
        }


        public List<SelectListItem> GetLISTITEMSLISTUserCodesForUserCodeGroup(
            string pUserCodeGroupID,
            bool pAddBlankCode = true,
            bool pIncludeSoftDeleted = true)
        {
            var a = new List<SelectListItem>();
            var mlijst = GetUserCodesForUserCodeGroup(UserCodeGroupCode.cMETHODOLOGYLijst, false, false);
            if (mlijst != null)
            {
                
                int i = 0;
                foreach (var m in mlijst)
                {
                    a.Insert(i, (new SelectListItem { Text = m.DESCRIPTION, Value = m.USERCODEID }));
                    i++;
                };
            };
            return a;


        }

        //public List<USERCODE> GetUserCodesForListUserCodeGroup(
        //  string pUserCodeGroupID,
        //  bool pAddBlankCode = true,
        //  bool pIncludeSoftDeleted = true,
        //  string pParentUserCodeID = null,
        //  List<string> pDependencyCodes = null)
        //{
        //    if (pUserCodeGroupID.IsNullOrEmptyOrBlankCode())
        //    {
        //        return null;
        //    }
        //    var vResult = new List<USERCODE>();
        //    if (pAddBlankCode)
        //    {
        //        vResult.Add(UserCode.BlankUserCode);
        //    }

        //    IEnumerable<WGK.Lib.UserCodes.UserCode> vQuery = this.iUserCodeCache
        //        .Where(p => p.UserCodeGroupID == pUserCodeGroupID);

        //    if (!pParentUserCodeID.IsNullOrEmptyOrBlankCode())
        //    {
        //        // Filter UserCodes that have the specified parent UserCode
        //        vQuery = vQuery
        //            .Where(p => p.ParentUserCodeID == pParentUserCodeID);
        //    }

        //    if (!pIncludeSoftDeleted)
        //    {
        //        // Filter UserCodes that are not soft deleted
        //        vQuery = vQuery
        //            .Where(p => !(p.SoftDeleted == "0"));
        //    }


        //    if (pDependencyCodes != null)
        //    {
        //        foreach (string vCode in pDependencyCodes)
        //        {
        //            IEnumerable<WGK.Lib.UserCodes.UserCode> vTempQuery = vQuery.Where(p => (p.DependencySet != null) && p.DependencySet.Contains(vCode));
        //            vResult.AddRange(vTempQuery.OrderBy(p => p.SortOrder).ThenBy(p => p.Description));

        //        }
        //    }

        //    vResult = vResult.Distinct().ToList();
        //    return vResult;
        //}

        public USERCODE GetUserCode(
            string pUserCodeID,
            string pUserCodeGroupID)
        {
            
            USERCODE WantedUSERCODE =  _context.USERCODE
                .Where(p => p.USERCODEID == pUserCodeID
                        &&  p.USERCODEGROUPID == pUserCodeGroupID).FirstOrDefault();
            return WantedUSERCODE;


        }


        public string GetUserCodeDescription(
            string pUserCodeID,
            string pUserCodeGroupID)
        {
            if(pUserCodeID.IsNullOrEmpty() || pUserCodeID.Trim().IsNullOrEmpty())
            {

                return "";
            }
            if (pUserCodeGroupID.IsNullOrEmpty() || pUserCodeGroupID.Trim().IsNullOrEmpty())
            {

                return "";
            }

            USERCODE vUserCode = this.GetUserCode(pUserCodeID, pUserCodeGroupID);
            if (vUserCode == null)
            {
                return "";
            }
            return vUserCode.DESCRIPTION;
        }








    }
}
