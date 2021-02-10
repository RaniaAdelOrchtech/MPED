using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MPMAR.Data
{
    /// <summary>
    /// Class for EgyptVision table which form EgyptVision object used in EgyptVision screens
    /// </summary>
    public class EgyptVision : PageSeo
    {
        public int Id { get; set; }
        public int PageRouteVersionId { get; set; }
        public string EnEgyptVisionName { get; set; }
        public string ArEgyptVisionName { get; set; }

        public string EnEgyptVisionSmallDesc { get; set; }
        public string ArEgyptVisionSmallDesc { get; set; }

        public string EnEgyptVisionDesc { get; set; }
        public string ArEgyptVisionDesc { get; set; }
        public int? StatusId { get; set; }
        public string EnImagePath { get; set; }
        public string ArImagePath { get; set; }
        public string BgColor { get; set; }
        public string LineColor { get; set; }
        public bool ImagePositionIsRight { get; set; }
        public int? Order { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }

    }
}