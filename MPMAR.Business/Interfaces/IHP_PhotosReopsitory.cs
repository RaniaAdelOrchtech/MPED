using MPMAR.Data.HomePageModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace MPMAR.Business.Interfaces
{
    public interface IHP_PhotosReopsitory
    {
        /// <summary>
        /// get all HomePagePhoto
        /// </summary>
        /// <returns></returns>
        IEnumerable<HomePagePhoto> GetAll();
        /// <summary>
        /// get HomePagePhoto by id
        /// </summary>
        /// <param name="id">HomePagePhoto id</param>
        /// <returns></returns>
        HomePagePhoto GetById(int id);
        /// <summary>
        /// update HomePagePhoto
        /// </summary>
        /// <param name="homePagePhoto"></param>
        void Update(HomePagePhoto homePagePhoto);
        /// <summary>
        /// get HomePagePhotoVersions by HomePagePhoto id
        /// </summary>
        /// <param name="id">HomePagePhoto id</param>
        /// <returns></returns>
        HomePagePhotoVersions GetByPhotoId(int id);
    }
}
