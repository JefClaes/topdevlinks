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

    public class PostLinkViewModel
    {
        public PostLinkViewModel(string title, string url)
        {
            Title = title;
            Url = url;            
        }

        public string Title { get; set; }

        public string Url { get; set; }
    }

    public class PostCategoryViewModel
    {
        public PostCategoryViewModel(string name, int priority)
        {
            Links = new List<PostLinkViewModel>();
            Name = name;
            Priority = priority;
        }

        public string Name { get; set; }

        public IList<PostLinkViewModel> Links { get; set; }

        public int Priority { get; set; }
    }


}