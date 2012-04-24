using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace TopDevLinks.Areas.Admin.Models.ViewModels
{
    public class CategoriesIndexViewModel
    {
        public CategoriesViewModel Categories { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public int? Priority { get; set; }
    }
}