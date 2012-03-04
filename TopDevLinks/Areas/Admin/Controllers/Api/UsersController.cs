using System.Collections.Generic;
using System.Linq;
using TopDevLinks.Areas.Admin.Models.Representations;
using TopDevLinks.Infrastructure;
using TopDevLinks.Models.Entities;
using TopDevLinks.Queries;

namespace TopDevLinks
{
    public class UsersController : MongoContextApiController
    {
        public IEnumerable<UserRepresentation> Get()
        {
            return Execute<IEnumerable<User>>(new GetAllUsersQuery())
                .Select(u => new UserRepresentation() 
                {
                    UserName = u.Login,
                    Email = u.Email
                }
            );
        }
    }
}
