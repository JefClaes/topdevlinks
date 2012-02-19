using System;
using System.Collections.Generic;
using MongoDB.Bson;

namespace TopDevLinks.Models.Documents
{
    public class Post
    {
        public ObjectId Id { get; set; }
        public List<Link> Links { get; set; }
        public bool Published { get; set; }
        public DateTime PublishDate { get; set; }
    }
}