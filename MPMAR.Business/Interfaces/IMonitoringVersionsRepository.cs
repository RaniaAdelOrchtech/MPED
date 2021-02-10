using MPMAR.Data;
using MPMAR.Data.HomePageModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace MPMAR.Business.Interfaces
{
    public interface IMonitoringVersionsRepository
    {
        /// <summary>
        /// add new MonitoringVersions
        /// </summary>
        /// <param name="model"></param>
        void Add(MonitoringVersions model);
        /// <summary>
        /// update MonitoringVersions
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        bool Update(MonitoringVersions model);
        /// <summary>
        /// get list of MonitoringVersions
        /// </summary>
        /// <returns></returns>
        List<MonitoringVersions> GetMonitringVersions();
        /// <summary>
        /// get MonitoringVersions by id
        /// </summary>
        /// <param name="id">MonitoringVersions id</param>
        /// <returns></returns>
        MonitoringVersions Get(int id);
        /// <summary>
        /// get MonitoringVersions by Monitoring id
        /// </summary>
        /// <param name="id">Monitoring id</param>
        /// <returns></returns>
        MonitoringVersions GetByMonitringId(int id);
        /// <summary>
        /// get list of all drafts MonitoringVersions
        /// </summary>
        /// <returns></returns>
        IEnumerable<MonitoringVersions> GetAllDrafts();
        /// <summary>
        /// get list of all submited MonitoringVersions
        /// </summary>
        /// <returns></returns>
        IEnumerable<MonitoringVersions> GetAllSubmitted();
    }
}
