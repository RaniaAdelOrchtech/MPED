using MPMAR.Data.HomePageModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace MPMAR.Business.Interfaces
{
   public interface IPublicationRepository
    {
        /// <summary>
        /// get all not deleted Publication
        /// </summary>
        /// <returns></returns>
        IEnumerable<Publication> GetAll();
        /// <summary>
        /// get Publication by id
        /// </summary>
        /// <param name="id">Publication id</param>
        /// <returns></returns>
        Publication GetById(int id);

        /// <summary>
        /// add new Publication
        /// </summary>
        /// <param name="publication"></param>
        void Add(Publication publication);
        /// <summary>
        /// update Publication
        /// </summary>
        /// <param name="publication"></param>
        void Update(Publication publication);
        /// <summary>
        /// get PublicationVersions by Publication id
        /// </summary>
        /// <param name="id">Publication id</param>
        /// <returns></returns>
        PublicationVersions GetByPublicationId(int id);
    }
}
