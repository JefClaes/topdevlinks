﻿using System;
using System.Collections.Generic;
using MongoDB.Bson;

namespace TopDevLinks.Models.Documents
{
    public class Post
    {
        public ObjectId Id { get; private set; }
        public IEnumerable<Link> Links { get; private set; }
        public bool Published { get; set; }
        public DateTime PublishDate { get; set; }

        protected List<Link> TypedLinks { get { return (List<Link>) Links; } } 

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