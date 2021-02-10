using MPMAR.Data;
using MPMAR.Data.HomePageModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace MPMAR.Business.Interfaces
{
    public interface IHP_VideoVersionRepository
    {
        /// <summary>
        /// add new HomePageVideoVersions
        /// </summary>
        /// <param name="model"></param>
        void Add(HomePageVideoVersions model);
        /// <summary>
        /// update HomePageVideoVersions
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        bool Update(HomePageVideoVersions model);
        /// <summary>
        /// get list of HomePageVideoVersions
        /// </summary>
        /// <returns></returns>
        List<HomePageVideoVersions> GetVideosVersions();
        /// <summary>
        /// get HomePageVideoVersions by id
        /// </summary>
        /// <param name="id">HomePageVideoVersions id</param>
        /// <returns></returns>
        HomePageVideoVersions Get(int id);
        /// <summary>
        /// get list of HomePageVideoVersions by HomePageVideo id
        /// </summary>
        /// <param name="id">HomePageVideo id</param>
        /// <returns></returns>
        HomePageVideoVersions GetByVideoId(int id);
        /// <summary>
        /// get list of all drafts HomePageVideoVersions
        /// </summary>
        /// <returns></returns>
        IEnumerable<HomePageVideoVersions> GetAllDrafts();
        /// <summary>
        /// get list of all submited HomePageVideoVersions
        /// </summary>
        /// <returns></returns>
        IEnumerable<HomePageVideoVersions> GetAllSubmitted();
    }
}
