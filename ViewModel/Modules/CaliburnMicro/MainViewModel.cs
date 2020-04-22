using Caliburn.Micro;
using DataAccess.Events;
using Logic.Services;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace ViewModel.Modules.CaliburnMicro
{
    public interface IMainViewModel
    {
        string Title { get; }
        string Text { get; set; }
        void Change();
    }

    public class MainViewModel : Screen, IMainViewModel, IHandle<IDatabaseEvent<TextUpdatedEvent>>
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly IService _service;

        private string _text;

        public MainViewModel(IEventAggregator eventAggregator, IService service)
        {
            Title = "MVVM with Caliburn Micro";

            _eventAggregator = eventAggregator;
            _service = service;

            _eventAggregator.SubscribeOnPublishedThread(this);
        }

        public string Title { get; }

        public string Text
        {
            get => _text;
            set
            {
                _text = value;
                NotifyOfPropertyChange(() => Text);
            }
        }

        public void Change()
        {
            _service.UpdateDataText(_text);
        }

        public Task HandleAsync(IDatabaseEvent<TextUpdatedEvent> message, CancellationToken cancellationToken)
        {
            return Task.Factory.StartNew(() =>
            {
                MessageBox.Show($" Old Text: { message.Event.OldText }\n New Text: { message.Event.NewText }");
            });
        }
    }
}
