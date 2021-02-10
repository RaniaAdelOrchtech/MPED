using MPMAR.Business.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MPMAR.Business.Interfaces
{
    public interface IGlobalElasticSearchService
    {
        /// <summary>
        /// delete data from Global Elastic Search
        /// </summary>
        /// <param name="pageRouteId"></param>
        /// <returns></returns>
        Task DeleteAsync(int pageRouteId);
        /// <summary>
        /// add all Global Elastic Search data
        /// </summary>
        /// <param name="globalSearch"></param>
        /// <returns></returns>
        Task AddManyAsync(GlobalSearchModel[] globalSearch);

        /// <summary>
        /// add  Global Elastic Search data
        /// </summary>
        /// <param name="globalSearch"></param>
        /// <returns></returns>
        Task AddAsync(GlobalSearchModel globalSearch);
        /// <summary>
        /// find  Global Elastic Search data
        /// </summary>
        /// <param name="query"></param>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        Task<SearchViewModel> FindAsync(string query, int page = 1, int pageSize = 10);
        /// <summary>
        /// delete all Global Elastic Search data
        /// </summary>
        /// <returns></returns>
        Task DeleteAllAsync();
    }
}
