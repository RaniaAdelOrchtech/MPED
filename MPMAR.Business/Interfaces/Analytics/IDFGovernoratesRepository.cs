using MPMAR.Analytics.Data;
using System.Collections.Generic;

namespace MPMAR.Business.Interfaces
{
    public interface IDFGovernoratesRepository
    {
        /// <summary>
        /// get all not deleted DFGovernorate
        /// </summary>
        /// <returns></returns>
        IEnumerable<DFGovernorate> GetAll();
        /// <summary>
        /// get all DFGovernorate where is total is null or false
        /// </summary>
        /// <returns></returns>

        public IEnumerable<DFGovernorate> GetAllGover();
        /// <summary>
        /// get all regions
        /// </summary>
        /// <returns></returns>
        public IEnumerable<DFGovernorate> GetAllRegion();
        /// <summary>
        /// get  DFGovernorate by region id with is total = false
        /// </summary>
        /// <param name="id">region id</param>
        /// <returns></returns>
        public IEnumerable<DFGovernorate> GetGovernsByRegionId(int id);
        /// <summary>
        /// get  DFGovernorate by region id with is total = true
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public DFGovernorate GetGovernsByRegionIdWithTrue(int id);
        /// <summary>
        /// get DFGovernorate by id
        /// </summary>
        /// <param name="govID">DFGovernorate id</param>
        /// <returns></returns>
        public DFGovernorate GetGoverById(int govID);


    }
}
