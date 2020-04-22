using DataAccess.Events;

namespace DataAccess.EventPublishers
{
    public interface IEventPublisher
    {
        void Publish<TEvent>(IDatabaseEvent<TEvent> message);
    }
}
