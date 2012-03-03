namespace TopDevLinks.Infrastructure
{
    public abstract class Query<TResult> : MongoContext
    {
        public abstract TResult Execute();
    }
}