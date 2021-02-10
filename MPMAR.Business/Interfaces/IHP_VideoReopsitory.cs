using MPMAR.Data.HomePageModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace MPMAR.Business.Interfaces
{
    public interface IHP_VideoReopsitory
    {
        /// <summary>
        /// get all HomePageVideo
        /// </summary>
        /// <returns></returns>
        IEnumerable<HomePageVideo> GetAll();
        /// <summary>
        /// get HomePageVideo by id
        /// </summary>
        /// <param name="id">HomePageVideo id</param>
        /// <returns></returns>
        HomePageVideo GetById(int id);
        /// <summary>
        /// update HomePageVideo
        /// </summary>
        /// <param name="homePagePhoto"></param>
        void Update(HomePageVideo homePagePhoto);
        /// <summary>
        /// get HomePageVideoVersions by HomePageVideo id
        /// </summary>
        /// <param name="id">HomePageVideo id</param>
        /// <returns></returns>
        HomePageVideoVersions GetByVideoId(int id);
    }
}
