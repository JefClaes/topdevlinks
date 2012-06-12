using System;
using System.Collections.Generic;
using MongoDB.Bson;

namespace TopDevLinks.Models.Entities
{
    public class Post : Entity
    {
        public IEnumerable<Link> Links { get; private set; }
        public bool Published { get; set; }
        public DateTime? PublishDate { get; set; }

        private List<Link> TypedLinks { get { return (List<Link>)Links; } } 

        public Post()
        {
            Links = new List<Link>();
        }

        public void AddLink(Link link)
        {            
            TypedLinks.Add(link);
        }

        public void RemoveLink(Link link)
        {
            TypedLinks.Remove(link);
        }
    }
}