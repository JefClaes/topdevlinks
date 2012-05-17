﻿using MongoDB.Driver.Builders;
using TopDevLinks.Infrastructure;
using TopDevLinks.Models.Entities;
using TopDevLinks.Models.ViewModels;

namespace TopDevLinks.Queries
{
    public class GetUpcomingPostQuery : Query<PostViewModel>
    {
        public override PostViewModel Execute()
        {
            var upcomingPost = MongoContext.GetCollection<Post>().FindOne(Query.EQ("Published", false));
            // TODO...
            return null;
        }
    }
}