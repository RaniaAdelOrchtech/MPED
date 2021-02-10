using System;
using System.Collections.Generic;
using System.Text;

namespace MPMAR.Data
{
    /// <summary>
    /// Class for Logs table which form Log object used to tell admin all operations done in the system
    /// </summary>
    public class Log
    {
        public int Id { get; set; }
        public string Application { get; set; }
        public DateTime Logged { get; set; }
        public string Level { get; set; }
        public string Message { get; set; }
        public string Logger { get; set; }
        public string CallSite { get; set; }
        public string Exception { get; set; }
    }
}
