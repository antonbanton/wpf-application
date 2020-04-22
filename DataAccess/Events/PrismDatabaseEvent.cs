using Prism.Events;

namespace DataAccess.Events
{
    public class PrismDatabaseEvent<TEvent> : PubSubEvent<IDatabaseEvent<TEvent>>
    {

    }
}
