using MPMAR.Data;
using MPMAR.Data.HomePageModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace MPMAR.Business.Interfaces
{
    public interface IHP_PhotosVersionRepository
    {
        /// <summary>
        /// Add new photo version object to database
        /// </summary>
        /// <param name="model">photo version model</param>
        void Add(HomePagePhotoVersions model);

        /// <summary>
        /// Update an existing photo version object from database
        /// </summary>
        /// <param name="model">photo version new data</param>
        bool Update(HomePagePhotoVersions model);
        
        /// <summary>
        /// Get all photo version objects 
        /// </summary>
        /// <returns></returns>
        List<HomePagePhotoVersions> GetPhotosVersions();

        /// <summary>
        /// Get photo version object by id
        /// </summary>
        /// <param name="id">photo version id</param>
        /// <returns></returns>
        HomePagePhotoVersions Get(int id);

        /// <summary>
        /// Get photo version object by photo id
        /// </summary>
        /// <param name="id">photo id</param>
        /// <returns></returns>
        HomePagePhotoVersions GetByPhotoId(int id);

        /// <summary>
        /// Get all drafted photo objects
        /// </summary>
        /// <returns></returns>
        IEnumerable<HomePagePhotoVersions> GetAllDrafts();

        /// <summary>
        /// Get all submitted photo objects
        /// </summary>
        /// <returns></returns>
        IEnumerable<HomePagePhotoVersions> GetAllSubmitted();
    }
}
