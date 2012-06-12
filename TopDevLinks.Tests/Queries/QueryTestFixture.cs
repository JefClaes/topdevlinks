using TopDevLinks.Infrastructure;

namespace TopDevLinks.Tests.Queries
{
    public abstract class QueryTestFixture
    {
        protected MongoTestContext MongoContext { get; private set; }
        protected EntityStore EntityStore { get; private set; }

        protected QueryTestFixture()
        {
            MongoContext = new MongoTestContext();
            EntityStore = new EntityStore(MongoContext);
        }

        protected TResult Execute<TResult>(Query<TResult> query)
        {
            query.MongoContext = MongoContext;
            return query.Execute();
        }
    }
}