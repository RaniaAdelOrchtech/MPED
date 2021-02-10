using MPMAR.Data;
using MPMAR.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MPMAR.Business
{
   public interface IUserManagmentRepository
    {
        

        /// <summary>
        ///  edit account
        /// </summary>
        /// <param name="editUser">user model</param>
        /// <returns></returns>
        Task<bool> Edit(EditUserRoleViewModel editUser);

        /// <summary>
        ///  delete account
        /// </summary>
        /// <param name="id">user id</param>
        /// <returns></returns>
        Task<bool> Delete(string id);

    }
}
