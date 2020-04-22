namespace DataAccess.Events
{
    public interface IDatabaseEvent<TEvent>
    {
        TEvent Event { get; set; }
    }

    public class DatabaseEvent<TEvent> : IDatabaseEvent<TEvent>
    {
        public TEvent Event { get; set; }
    }
}
