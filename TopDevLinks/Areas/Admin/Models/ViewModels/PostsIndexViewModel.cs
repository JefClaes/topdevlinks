using System.Web.Mvc;
using TopDevLinks.Models.ViewModels;

namespace TopDevLinks.Areas.Admin.Models.ViewModels
{
    public class PostsIndexViewModel
    {
        public SelectList Categories { get; set; }

        public int SelectedCategoryId { get; set; }

        public PostsViewModel UnpublishedPosts { get; set; }
    }
}