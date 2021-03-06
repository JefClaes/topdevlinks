﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TopDevLinks.Areas.Admin.Models.ViewModels
{
    public class CategoriesIndexViewModel
    {
        public IEnumerable<CategoryViewModel> Categories { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public int? Priority { get; set; }
    }
}