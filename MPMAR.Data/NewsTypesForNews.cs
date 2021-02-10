using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace MPMAR.Data
{
    /// <summary>
    /// Class for NewsTypesForNews table which form NewsTypesForNews object used in NewsTypesForNews screens
    /// </summary>
    public class NewsTypesForNews
    { 
        public int Id { get; set; }
        public int PageNewsId { get; set; }
        public int NewsTypeId { get; set; }
        public PageNewsType NewsType { get; set; }
    }
}
