using MPMAR.Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MPMAR.Business.Interfaces
{
    public interface IPhotoArchiveElasticSearchService
    {
        /// <summary>
        /// delete data from Photo Archive
        /// </summary>
        /// <param name="pageRouteId"></param>
        /// <returns></returns>
        Task DeleteAsync(PhotoArchive photoArchive);
        /// <summary>
        /// add all data to Photo Archive
        /// </summary>
        /// <param name="photoArchive"></param>
        /// <returns></returns>
        Task AddManyAsync(PhotoArchive[] photoArchive);

        /// <summary>
        /// add  data to Photo Archive
        /// </summary>
        /// <param name="photoArchive"></param>
        /// <returns></returns>
        Task AddAsync(PhotoArchive photoArchive);
        /// <summary>
        /// find data from Photo Archive
        /// </summary>
        /// <param name="query"></param>
        /// <param name="archType"></param>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        Task<IReadOnlyCollection<PhotoArchive>> Find(string query, string archType, int page = 1, int pageSize = 50);

     
    }
}
