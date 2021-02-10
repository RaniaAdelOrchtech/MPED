using MPMAR.Data.HomePageModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace MPMAR.Business.Interfaces
{
    public interface IHP_AffiliatesReopsitory
    {
        /// <summary>
        /// get all HomePageAffiliates
        /// </summary>
        /// <returns></returns>
        IEnumerable<HomePageAffiliates> GetAll();
        /// <summary>
        /// get HomePageAffiliates by id
        /// </summary>
        /// <param name="id">HomePageAffiliates id</param>
        /// <returns></returns>
        HomePageAffiliates GetById(int id);
        /// <summary>
        /// get HomePageAffiliates by id,
        /// with no tracking
        /// </summary>
        /// <param name="id">HomePageAffiliates id</param>
        /// <returns></returns>
        HomePageAffiliates GetByIdWithNoTracking(int id);
        /// <summary>
        /// update HomePageAffiliates
        /// </summary>
        /// <param name="homePageAffiliates"></param>
        void Update(HomePageAffiliates homePageAffiliates);
        /// <summary>
        /// add new HomePageAffiliates
        /// </summary>
        /// <param name="homePageAffiliates"></param>
        void Add(HomePageAffiliates homePageAffiliates);
        /// <summary>
        /// delete HomePageAffiliates by id
        /// </summary>
        /// <param name="id">HomePageAffiliates id</param>
        /// <returns></returns>
        bool SoftDelete(int id);

    }
}
