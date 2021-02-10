using MPMAR.Data.HomePageModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace MPMAR.Business.Interfaces
{
    public interface IHP_PhotoSliderReopsitory
    {
        /// <summary>
        /// get all not deleted HomePagePhotoSlider
        /// </summary>
        /// <returns></returns>
        IEnumerable<HomePagePhotoSlider> GetAll();
        /// <summary>
        /// get HomePagePhotoSlider by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        HomePagePhotoSlider GetById(int id);
        /// <summary>
        /// get HomePagePhotoSlider by id,
        /// with no tracking
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        HomePagePhotoSlider GetByIdWithNoTracking(int id);
        /// <summary>
        /// update HomePagePhotoSlider
        /// </summary>
        /// <param name="homePagePhoto"></param>
        void Update(HomePagePhotoSlider homePagePhoto);
        /// <summary>
        /// delete HomePagePhotoSlider by id
        /// </summary>
        /// <param name="id">HomePagePhotoSlider id</param>
        /// <returns></returns>
        bool SoftDelete(int id);
        /// <summary>
        /// add new HomePagePhotoSlider
        /// </summary>
        /// <param name="photoSlider"></param>
        void Add(HomePagePhotoSlider photoSlider);
    }
}
