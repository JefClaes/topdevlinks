namespace TopDevLinks.Infrastructure
{
    public abstract class Query<TResult>
    {
        public MongoContext MongoContext { get; set; }
        public abstract TResult Execute();
    }
}