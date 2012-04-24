﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TopDevLinks.Areas.Admin.Models.ViewModels
{
    public class CategoriesViewModel
    {
        public CategoriesViewModel(IEnumerable<CategoryViewModel> items)
        {
            Items = items;
        }

        public IEnumerable<CategoryViewModel> Items { get; set; }
    }

    public class CategoryViewModel
    {
        public CategoryViewModel(string id, string name)
        {
            Id = id;
            Name = name;
        }

        public string Id { get; set; }

        public string Name { get; set; }
    }
}