using MPMAR.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace MPMAR.Business.Interfaces
{
    public interface IMinistryTimeLineRepository
    {
        /// <summary>
        ///add new  MinistryTimeLine
        /// </summary>
        /// <param name="ministryTimeLine"></param>
        /// <returns></returns>
        MinistryTimeLine Add(MinistryTimeLine ministryTimeLine);
        /// <summary>
        /// update MinistryTimeLine
        /// </summary>
        /// <param name="ministryTimeLine"></param>
        /// <returns></returns>
        MinistryTimeLine Update(MinistryTimeLine ministryTimeLine);
        /// <summary>
        /// get list of MinistryTimeLine orderd by MinistryTimeLine order
        /// </summary>
        /// <returns></returns>
        IEnumerable<MinistryTimeLine> GetMinistryTimeLine();
        /// <summary>
        /// delete MinistryTimeLine with specific id
        /// </summary>
        /// <param name="id">MinistryTimeLine id</param>
        /// <returns></returns>
        MinistryTimeLine Delete(int id);
        /// <summary>
        /// delete all MinistryTimeLines
        /// </summary>
        void DeleteAll();
        /// <summary>
        ///  get fisrt MinistryTimeLine for specific id if exist null otherwise
        /// </summary>
        /// <param name="id">MinistryTimeLine id></param>
        /// <returns></returns>
        MinistryTimeLine Get(int id);
        /// <summary>
        /// get fisrt MinistryTimeLine for specific id if exist null otherwise
        /// </summary>
        /// <param name="id">MinistryTimeLine id</param>
        /// <returns></returns>
        MinistryTimeLine GetDetail(int id);
        /// <summary>
        /// get fisrt MinistryTimeLine for specific id if exist null otherwise,
        /// with no tracking 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        MinistryTimeLine GetDetailWithNoTarcking(int id);
        /// <summary>
        /// get all not deleted MinistryTimeLines
        /// </summary>
        /// <returns></returns>
        IEnumerable<MinistryTimeLine> GetAll();
    }
}
