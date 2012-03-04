using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace TopDevLinks.Infrastructure
{
    public class MongoContextApiController : ApiController
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