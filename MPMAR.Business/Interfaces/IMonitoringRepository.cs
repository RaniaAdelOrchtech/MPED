using MPMAR.Data.HomePageModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace MPMAR.Business.Interfaces
{
   public interface IMonitoringRepository
    {
        /// <summary>
        /// get first Monitoring if exists null otherwise
        /// </summary>
        /// <returns></returns>
        Monitoring Get();
        /// <summary>
        /// update Monitoring 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        void Update(Monitoring monitoring);
        /// <summary>
        /// get MonitoringVersions by Monitoring id
        /// </summary>
        /// <param name="id">Monitoring id</param>
        /// <returns></returns>
        MonitoringVersions GetByMonitringId(int id);
    }
}
