using MPMAR.Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MPMAR.Business.Interfaces
{
    public interface IPageNewsElasticSearchService
    {
        /// <summary>
        /// delete data from news
        /// </summary>
        /// <param name="pageNews"></param>
        /// <returns></returns>
        Task DeleteAsync(PageNews pageNews);
        /// <summary>
        /// add all data to news
        /// </summary>
        /// <param name="pageNews"></param>
        /// <returns></returns>
        Task AddManyAsync(PageNews[] pageNews);
        /// <summary>
        /// add  data to news
        /// </summary>
        /// <param name="pageNews"></param>
        /// <returns></returns>
        Task AddAsync(PageNews pageNews);
        /// <summary>
        /// find data from news
        /// </summary>
        /// <param name="query"></param>
        /// <param name="newsTypeId"></param>
        /// <param name="lang"></param>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        Task<IReadOnlyCollection<PageNews>> Find(string query, int newsTypeId, string lang, int page = 1, int pageSize = 10);
        /// <summary>
        /// get data count for news
        /// </summary>
        /// <param name="query"></param>
        /// <param name="newsTypeId"></param>
        /// <param name="lang"></param>
        /// <returns></returns>
        Task<long> GetCount(string query, int newsTypeId, string lang);

        /// <summary>
        /// add new news page
        /// </summary>
        /// <param name="news"></param>
        /// <returns></returns>
        Task AddPageNewsDataToElasticSearch(PageNews[] news);
    }
}
