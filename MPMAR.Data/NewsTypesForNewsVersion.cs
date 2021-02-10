using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MPMAR.Data
{
    /// <summary>
    /// Class for NewsTypesForNewsVersions table which form NewsTypesForNewsVersions object used in NewsTypesForNews screens
    /// </summary>
    public class NewsTypesForNewsVersion
    { 
        public int Id { get; set; }

        public int PageNewsVersionId { get; set; }
        public int NewsTypeId { get; set; }

        [ForeignKey("NewsTypeId")]
        public PageNewsType NewsType { get; set; }
        [ForeignKey("PageNewsVersionId")]
        public PageNewsVersion PageNewsVersion { get; set; }

    }
}
