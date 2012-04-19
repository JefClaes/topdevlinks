using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TopDevLinks.Infrastructure;

namespace TopDevLinks.Tests.Commands
{    
   public abstract class CommandTestFixture
    {
        protected MongoContext MongoContext { get; private set; }
        protected EntityStore EntityStore { get; private set; }

        protected CommandTestFixture()
        {
            MongoContext = new MongoContext();
            EntityStore = new EntityStore(MongoContext);
        }

        protected void Execute(Command command)
        {
            command.MongoContext = MongoContext;
            command.EntityStore = EntityStore;
            command.Execute();
        }
    }
}
