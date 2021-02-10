using MPMAR.Data.HomePageModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace MPMAR.Business.Interfaces
{
    public interface IHP_LogoLinkReopsitory
    {
        /// <summary>
        /// Get all logolinks objects
        /// </summary>
        /// <returns></returns>
        IEnumerable<HomePageLogoLink> GetAll();

        /// <summary>
        /// Get logo link object by id
        /// </summary>
        /// <param name="id">logo link id</param>
        /// <returns></returns>
        HomePageLogoLink GetById(int id);

        /// <summary>
        /// Update logo link object
        /// </summary>
        /// <param name="homePageLogoLink">logo link new data</param>
        void Update(HomePageLogoLink homePageLogoLink);

        /// <summary>
        /// Get logolink version by logo link id
        /// </summary>
        /// <param name="id">logo link id</param>
        /// <returns></returns>
        HomePageLogoLinkVersions GetByLogoLinkId(int id);
    }
}
