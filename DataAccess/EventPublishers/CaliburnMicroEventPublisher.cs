using Caliburn.Micro;
using DataAccess.Events;

namespace DataAccess.EventPublishers
{
    public class CaliburnMicroEventPublisher : IEventPublisher
    {
        private readonly IEventAggregator _eventAggregator;

        public CaliburnMicroEventPublisher(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
        }

        public void Publish<TEvent>(IDatabaseEvent<TEvent> message)
        {
            _eventAggregator.PublishOnUIThreadAsync(message);
        }
    }
}
