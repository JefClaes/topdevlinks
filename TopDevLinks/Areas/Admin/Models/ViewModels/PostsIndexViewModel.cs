using System.Web.Mvc;
using TopDevLinks.Models.ViewModels;
using System.ComponentModel.DataAnnotations;

namespace TopDevLinks.Areas.Admin.Models.ViewModels
{
    public class PostsIndexViewModel
    {
        [Required]
        public string Title { get; set; }

        [Required]
        public string Url { get; set; }

        public SelectList Categories { get; set; }

        [Required]
        public string SelectedCategoryId { get; set; }

        public PostsViewModel UnpublishedPosts { get; set; }
    }
}