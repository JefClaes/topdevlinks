using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TopDevLinks.Areas.Admin.Models.ViewModels
{
    public class PostsIndexViewModel
    {
        public SelectList Categories { get; set; }

        public int SelectedCategoryId { get; set; }
    }
}