using MPMAR.Data.HomePageModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace MPMAR.Business.Interfaces
{
  public  interface IHP_BasicInfoReopsitory
    {
        /// <summary>
        /// get list of all HomePageBasicInfo
        /// </summary>
        /// <returns></returns>
        IEnumerable<HomePageBasicInfo> GetAll();
        /// <summary>
        /// get HomePageBasicInfo by id
        /// </summary>
        /// <param name="id">HomePageBasicInfo id</param>
        /// <returns></returns>
        HomePageBasicInfo GetById(int id);
        /// <summary>
        /// update HomePageBasicInfo
        /// </summary>
        /// <param name="homeBasicInfo"></param>

        void Update(HomePageBasicInfo homeBasicInfo);
    }
}
