using MPMAR.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace MPMAR.Business.Interfaces
{
   public interface IFooterMenuTitleRepository
    {
        /// <summary>
        /// Get All Footer menu title objects
        /// </summary>
        /// <returns></returns>
        IEnumerable<FooterMenuTitle> GetAll();

        /// <summary>
        /// Get footer menu title object by id
        /// </summary>
        /// <param name="id">footer menu title id</param>
        /// <returns></returns>
        FooterMenuTitle GetById(int id);

        /// <summary>
        /// update footer menu title from database
        /// </summary>
        /// <param name="model">footer menu title object new data</param>
        /// <returns></returns>
        bool Update(FooterMenuTitle model);

        /// <summary>
        /// delete footer menu title by id
        /// </summary>
        /// <param name="id">footer menu title id</param>
        /// <returns></returns>
        bool Delete(int id);

        /// <summary>
        /// get footer menu title version by footer menu title id
        /// </summary>
        /// <param name="id">footer menu title id</param>
        /// <returns></returns>
        FooterMenuTitleVersions GetByFooterMenuTitleId(int id);
    }
}
