using TopDevLinks.Infrastructure;

namespace TopDevLinks.Tests.Queries
{
    public abstract class QueryTestFixture
    {
        protected MongoContext MongoContext { get; private set; }
        protected EntityStore EntityStore { get; private set; }

        protected QueryTestFixture()
        {
            MongoContext = new MongoContext();
            EntityStore = new EntityStore(MongoContext);
        }

        protected TResult Execute<TResult>(Query<TResult> query)
        {
            query.MongoContext = MongoContext;
            return query.Execute();
        }
    }
}