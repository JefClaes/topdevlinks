using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TopDevLinks.Infrastructure
{
    public abstract class Command
    {
        public MongoContext MongoContext { get; set; }
        public abstract void Execute();
    }
}