using MPMAR.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


using System.Data;
using System.Linq.Dynamic.Core;


namespace MPMAR.Business
{
    public class LogRepository : ILogRepository
    {
        private readonly ApplicationDbContext db;

        public LogRepository(ApplicationDbContext _db)
        {
            db = _db;
        }
        public IEnumerable<Log> Get()
        {
            var data = db.Logs.Where(d => d.Level == "Info").ToList();
            return data;
        }

        public List<Log> GetAll(string searchValue, string sortColumnName, string sortDirection, int start, int lenght, out int totalCount)
        {
            var logs = db.Logs.AsQueryable();

            if (!string.IsNullOrEmpty(searchValue))//filter
            {
                logs = logs.Where(x => x.Message.ToLower().Contains(searchValue.ToLower()) || x.Logger.ToLower().Contains(searchValue.ToLower()) || x.Level.ToLower().Contains(searchValue.ToLower()));
            }
            totalCount = logs.Count();
            if (sortColumnName != "")
            {
                if (sortDirection == "asc")
                    logs = logs.OrderBy($"{sortColumnName} asc");
                else if (sortDirection == "desc")
                    logs = logs
                        .OrderBy($"{sortColumnName} descending");
            }
            //paging
            return logs.Skip(start).Take(lenght).ToList();
        }


    }
}
