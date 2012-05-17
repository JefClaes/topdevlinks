namespace TopDevLinks.Infrastructure
{
    public abstract class Query<TResult>
    {
        public MongoContext MongoContext { get; set; }
        public abstract TResult Execute();

        protected TOtherResult Execute<TOtherResult>(Query<TOtherResult> query)
        {
            query.MongoContext = MongoContext;
            return query.Execute();
        }
    }
}