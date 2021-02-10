using MPMAR.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace MPMAR.Business
{
  public interface ILogRepository
    {
        /// <summary>
        /// Get all logs object
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Log> Get();

        /// <summary>
        /// Get all logs object with server side
        /// </summary>
        /// <param name="searchValue">key for filter results with search value</param>
        /// <param name="sortColumnName">key for sort results append on sort coloumn</param>
        /// <param name="sortDirection"/>key for sort coloumn Asc or Desc</param>
        /// <param name="start">filter data with object index to start with it</param>
        /// <param name="lenght">filter data with number of objects you need</param>
        /// <param name="totalCount">out value for number of all objects</param>
        /// <returns></returns>
        List<Log> GetAll(string searchValue, string sortColumnName, string sortDirection, int start, int lenght, out int totalCount);
    }
}
