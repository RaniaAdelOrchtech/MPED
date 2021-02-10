using System;
using System.Collections.Generic;
using System.Text;

namespace MPMAR.Data
{
    /// <summary>
    /// Class for Statuses table which form Status object used in dropdowns for statuses
    /// </summary>
    public class Status
    {
        public int Id { get; set; }
        public string Name { get; set; }

        #region Navigation Properties
        public ICollection<PageRouteVersion> PageRouteVersions { get; set; }
        public ICollection<NavItemVersion> NavItemVersions { get; set; }
        #endregion
    }
}
