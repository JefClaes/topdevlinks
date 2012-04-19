using System;
using System.Web.Mvc;

namespace TopDevLinks.Infrastructure
{
    public class MongoContextController : Controller
    {
        private MongoContext _mongoContext;
        private EntityStore _entityStore;
        private Func<dynamic, dynamic> _executeQueryFunc;  

        public MongoContext MongoContext
        {
            get { return _mongoContext ?? (_mongoContext = new MongoContext()); }
            set { _mongoContext = value; }
        }

        public EntityStore EntityStore
        {
            get { return _entityStore ?? (_entityStore = new EntityStore(MongoContext)); }
        }

        public Func<dynamic, dynamic> ExecuteQueryFunc
        {
            get
            {
                if (_executeQueryFunc == null)
                {
                    _executeQueryFunc = query =>
                    {
                        query.MongoContext = MongoContext;
                        return query.Execute();
                    };
                }

                return _executeQueryFunc;
            }
            set { _executeQueryFunc = value; }
        }

        protected TResult Execute<TResult>(Query<TResult> query)
        {
            return (TResult)ExecuteQueryFunc(query);
        }

        protected void Execute(Command command) 
        {
            command.MongoContext = MongoContext;
            command.EntityStore = EntityStore;
            command.Execute();
        }
    }
}