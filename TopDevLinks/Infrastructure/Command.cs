namespace TopDevLinks.Infrastructure
{
    public abstract class Command
    {
        public MongoContext MongoContext { get; set; }
        public EntityStore EntityStore { get; set; }
        public abstract void Execute();

        protected void Execute(Command command)
        {
            command.MongoContext = MongoContext;
            command.EntityStore = EntityStore;
            command.Execute();
        }

        protected TResult Execute<TResult>(Query<TResult> query)
        {
            query.MongoContext = MongoContext;
            return query.Execute();
        }
    }
}