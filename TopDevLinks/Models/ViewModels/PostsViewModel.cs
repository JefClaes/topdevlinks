using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TopDevLinks.Models.ViewModels
{
    public class PostsViewModel
    {
        public PostsViewModel()
        {
            Posts = new List<PostViewModel>();
        }

        public IList<PostViewModel> Posts { get;set; }
    }

    public class PostViewModel
    {
        public PostViewModel(DateTime? publishDate) 
        {
            Categories = new List<PostCategoryViewModel>();
            PublishDate = publishDate;
        }

        public DateTime? PublishDate { get; set; }

        public IList<PostCategoryViewModel> Categories { get;set; }
    }

    public class PostCategoryViewModel
    {
        public PostCategoryViewModel(string name)
        {
            Links = new List<string>();
            Name = name;
        }

        public string Name { get; set; }

        public IList<string> Links { get; set; }
    }
}