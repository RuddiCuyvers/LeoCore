using System;
using System.Collections.Generic;


namespace WGK.Lib.Web.Mvc.Helpers
{
    public class AuthorizationEnablement
    {
        // Tuple Item1 is TaskID, Tuple Item2 is PermissionID
        private readonly List<Tuple<string, string>> iTaskPermissions;

        public AuthorizationEnablement()
        {
            this.iTaskPermissions = new List<Tuple<string, string>>();
        }

        public AuthorizationEnablement(string pTaskID, string pPermissionID)
            :this()
        {
            this.iTaskPermissions.Add(new Tuple<string, string>(pTaskID, pPermissionID));
        }

        //public AuthorizationEnablement(List<Tuple<string, string>> pTaskPermissions)
        //{
        //    this.iTaskPermissions = pTaskPermissions;
        //}

        public AuthorizationEnablement Add(string pTaskID, string pPermissionID)
        {
            this.iTaskPermissions.Add(new Tuple<string, string>(pTaskID, pPermissionID));
            return this;
        }

        public bool IsReadOnly()
        {
            if (this.iTaskPermissions.Count == 0)
            {
                // No permissions to check: don't disable the widget
                return false;
            }

            // Check if current user has at least one of the required permission(s) on task(s)
            //var vPrincipal = System.Threading.Thread.CurrentPrincipal as ITaskPrincipal;
            //if (vPrincipal == null)
            //{
            //    // Disable the widget if we cannot determine the User's task permissions (i.e., secure by default)
            //    return true;
            //}
            //if (!vPrincipal.Identity.IsAuthenticated)
            //{
            //    // Disable the widget if the User is not authenticated (i.e., secure by default)
            //    return true;
            //}

            foreach (var vTaskPermission in this.iTaskPermissions)
            {
                var vTaskID = vTaskPermission.Item1;
                var vPermissionID = vTaskPermission.Item2;
                if (string.IsNullOrEmpty(vTaskID) || string.IsNullOrEmpty(vPermissionID))
                {
                    // Skip incomplete entries
                    continue;
                }

                //if (vPrincipal.HasPermissionOnTask(vTaskID, vPermissionID))
                //{
                    // User has required permission: don't disable the widget
                    return false;
                //}
            }
            // Disable the widget if the User does not have any of the required permission(s) on task(s)
            return true;            
        }
    }
}