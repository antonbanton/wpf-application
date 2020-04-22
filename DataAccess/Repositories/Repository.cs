using DataAccess.EventPublishers;
using DataAccess.Events;
using DataAccess.Mocks;
using Model.Containers;

namespace DataAccess.Repositories
{
    public interface IRepository
    {
        IData<string> GetData();
        void UpdateDataText(string newText);
    }

    public class Repository : IRepository
    {
        private readonly IEventPublisher _eventPublisher;
        private readonly IDatabaseConnection _databaseConnection;

        public Repository(IEventPublisher eventPublisher, IDatabaseConnection databaseConnection)
        {
            _eventPublisher = eventPublisher;
            _databaseConnection = databaseConnection;
        }

        public IData<string> GetData()
        {
            return _databaseConnection.Data;
        }

        public void UpdateDataText(string newText)
        {
            var oldText = _databaseConnection.Data.Entity;
            _databaseConnection.Data.Entity = newText;

            var databaseEvent = new DatabaseEvent<TextUpdatedEvent>()
            {
                Event = new TextUpdatedEvent()
                {
                    OldText = oldText,
                    NewText = newText
                }
            };

            _eventPublisher.Publish(databaseEvent);
        }
    }
}
