using MPMAR.Data.HomePageModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace MPMAR.Business.Interfaces
{
   public interface IHP_PhotoSliderVersionReopsitory
    {
        /// <summary>
        /// get list of HomePagePhotoSliderVersion
        /// </summary>
        /// <returns></returns>
        IEnumerable<HomePagePhotoSliderVersion> GetAll();
        /// <summary>
        /// delete HomePagePhotoSliderVersion by id
        /// </summary>
        /// <param name="id">HomePagePhotoSliderVersion id</param>
        /// <returns></returns>
        bool SoftDelete(int id);
        /// <summary>
        /// add new HomePagePhotoSliderVersion
        /// </summary>
        /// <param name="photoSlider"></param>
        void Add(HomePagePhotoSliderVersion photoSlider);
        /// <summary>
        /// get HomePagePhotoSliderVersion by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        HomePagePhotoSliderVersion GetById(int id);
        /// <summary>
        /// update HomePagePhotoSliderVersion
        /// </summary>
        /// <param name="homePagePhotoSlider"></param>
        void Update(HomePagePhotoSliderVersion homePagePhotoSlider);
        /// <summary>
        /// get HomePagePhotoSliderVersion by HomePagePhotoSlider id
        /// </summary>
        /// <param name="id">HomePagePhotoSlider id</param>
        /// <returns></returns>
        HomePagePhotoSliderVersion GetBySliderId(int id);
        /// <summary>
        /// get list of all drafts HomePagePhotoSliderVersion
        /// </summary>
        /// <returns></returns>
        IEnumerable<HomePagePhotoSliderVersion> GetAllDrafts();
        /// <summary>
        /// get list of all submited HomePagePhotoSliderVersion
        /// </summary>
        /// <returns></returns>
        IEnumerable<HomePagePhotoSliderVersion> GetAllSubmitted();
    }
}
