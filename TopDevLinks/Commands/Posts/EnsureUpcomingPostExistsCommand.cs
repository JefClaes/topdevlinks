using TopDevLinks.Infrastructure;
using TopDevLinks.Models.Entities;
using TopDevLinks.Queries;

namespace TopDevLinks.Commands.Posts
{
    public class EnsureUpcomingPostExistsCommand : Command
    {
        public override void Execute()
        {
            if (Execute(new GetUpcomingPostQuery()) == null)
            {
                EntityStore.Save(new Post { Published = false});
            }
        }
    }
}