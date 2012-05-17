using TopDevLinks.Infrastructure;
using TopDevLinks.Models.Entities;
using TopDevLinks.Queries;

namespace TopDevLinks.Commands
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