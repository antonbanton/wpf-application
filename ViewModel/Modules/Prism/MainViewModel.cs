using DataAccess.Events;
using Logic.Services;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using System.Windows;
using System.Windows.Input;

namespace ViewModel.Modules.Prism
{
    public interface IMainViewModel
    {
        string Title { get; }
        string Text { get; set; }
        void Change();
    }

    public class MainViewModel : BindableBase, IMainViewModel
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly IService _service;

        private string _text;
        public ICommand ChangeCommand { get; set; }

        public MainViewModel(IEventAggregator eventAggregator, IService service)
        {
            Title = "MVVM with Prism";

            _eventAggregator = eventAggregator;
            _service = service;

            _eventAggregator.GetEvent<PrismDatabaseEvent<TextUpdatedEvent>>().Subscribe(Handle);
            ChangeCommand = new DelegateCommand(Change);
        }

        public string Title { get; }

        public string Text
        {
            get => _text;
            set => SetProperty(ref _text, value);
        }

        public void Change()
        {
            _service.UpdateDataText(_text);
        }

        public void Handle(IDatabaseEvent<TextUpdatedEvent> message)
        {
            MessageBox.Show($" Old Text: { message.Event.OldText }\n New Text: { message.Event.NewText }");
        }
    }
}
