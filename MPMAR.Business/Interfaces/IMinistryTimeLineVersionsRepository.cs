using MPMAR.Data;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace MPMAR.Business.Interfaces
{
    public interface IMinistryTimeLineVersionsRepository
    {
        /// <summary>
        /// get all MinistryTimeLineVersions by page info
        /// </summary>
        /// <param name="pageInfo"></param>
        /// <returns></returns>
        IEnumerable<MinistryTimeLineVersions> GetAllByPageInfo(FormerMinistriesPageInfoVersions pageInfo);
        /// <summary>
        /// get MinistryTimeLineVersions details by id
        /// </summary>
        /// <param name="id">MinistryTimeLineVersions id</param>
        /// <returns></returns>
        MinistryTimeLineVersions GetDetail(int id);
        /// <summary>
        /// get MinistryTimeLineVersions details by id,
        /// with no tracking
        /// </summary>
        /// <param name="id">MinistryTimeLineVersions id</param>
        /// <returns></returns>
        MinistryTimeLineVersions GetDetailWithNoTarcking(int id);
        /// <summary>
        /// get MinistryTimeLineVersions By Condition
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        MinistryTimeLineVersions GetByCondition(Expression<Func<MinistryTimeLineVersions, bool>> expression);
        /// <summary>
        /// update MinistryTimeLineVersions 
        /// </summary>
        /// <param name="model">MinistryTimeLineVersions model</param>
        void Update(MinistryTimeLineVersions model);
        /// <summary>
        /// add new MinistryTimeLineVersions
        /// </summary>
        /// <param name="model">MinistryTimeLineVersions model</param>
        void Add(MinistryTimeLineVersions model);
        /// <summary>
        /// delete all MinistryTimeLineVersions
        /// </summary>
        void MarkAllAsDeleted();
    }
}
