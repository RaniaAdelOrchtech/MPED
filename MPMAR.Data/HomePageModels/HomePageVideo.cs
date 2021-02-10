using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MPMAR.Data.HomePageModels
{
    /// <summary>
    /// Class for HomePageVideo table which form HomePageVideo model used in HomePageVideo screen
    /// </summary>
    public class HomePageVideo : ActionInfo
    {
        public int Id { get; set; }
        [Required]
        public string VideoUrl { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
    }
}
