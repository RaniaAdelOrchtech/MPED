using MPMAR.Data;
using MPMAR.Data.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace MPMAR.Business.Interfaces
{
    public interface IBEUsersPrivilegesRepository
    {
        /// <summary>
        /// add Default Privileges for specefic user
        /// </summary>
        /// <param name="userId"></param>
        void AddDefaultPrivileges(string userId);
        /// <summary>
        /// get list of non super admin users
        /// </summary>
        /// <returns></returns>
        IEnumerable<ApplicationUser> GetNotSuperAdminUsers();
        /// <summary>
        /// get list of User Privileges for specefic user
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        IEnumerable<BEUsersPrivileges> GetUserPrivileges(string userId);
        /// <summary>
        /// update teUser Privileges for specefic user
        /// </summary>
        /// <param name="bEUsersPrivileges"></param>
        void UpdateUserPrivileges(List<BEUsersPrivileges> bEUsersPrivileges);
        /// <summary>
        /// reset all Privileges for all useres
        /// </summary>
        void ResetUsersPrivileges();
        /// <summary>
        /// update dynamic page privileges for all users
        /// </summary>
        /// <param name="pageId"></param>
        /// <param name="isRemove">true in case of adding a new DP false otherwise</param>
        void UpdateWithNewDynamicPages(int pageId, bool isRemove = false);
        public Dictionary<PrivilegesPageType, string> GetHomePageSectionsNames();
    }
}
