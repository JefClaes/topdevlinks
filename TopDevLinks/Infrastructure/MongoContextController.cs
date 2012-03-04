using System;
using System.Web.Mvc;

namespace TopDevLinks.Infrastructure
{
    public class MongoContextController : Controller
    {
        private MongoContext _mongoContext;
        private EntityStore _entityStore;

        public MongoContext MongoContext
        {
            get { return _mongoContext ?? (_mongoContext = new MongoContext()); }
            set { _mongoContext = value; }
        }

        public EntityStore EntityStore
        {
            get { return _entityStore ?? (_entityStore = new EntityStore(MongoContext)); }
        }

        protected TResult Execute<TResult>(Query<TResult> query)
        {
            query.MongoContext = MongoContext;
            return query.Execute();
        }
    }
}