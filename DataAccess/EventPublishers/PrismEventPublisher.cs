using DataAccess.Events;
using Prism.Events;

namespace DataAccess.EventPublishers
{
    public class PrismEventPublisher : IEventPublisher
    {
        private readonly IEventAggregator _eventAggregator;

        public PrismEventPublisher(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
        }

        public void Publish<TEvent>(IDatabaseEvent<TEvent> message)
        {
            _eventAggregator.GetEvent<PrismDatabaseEvent<TEvent>>().Publish(message);
        }
    }
}
