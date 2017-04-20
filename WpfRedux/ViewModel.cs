using System.ComponentModel;
using System.Windows.Input;

namespace WpfRedux
{
    public class ViewModel : INotifyPropertyChanged
    {
        public ViewModel(Store store)
        {
            _store = store;
            store.StateChanged += ChangeState;

            UpdateTextCommand = new RelayCommand(UpdateText);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            var handler = PropertyChanged;
            handler?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private Store _store;

        public string Text
        {
            get { return _store.State.Text; }
        }

        public ICommand UpdateTextCommand { get; }

        public void ChangeState(object sender, ChangeStateArgs args)
        {
            if(args?.OldState?.Text != args?.NewState?.Text)
            {
                OnPropertyChanged(nameof(Text));
            }
        }

        private void UpdateText(object param)
        {
            var text = (string)param;
            var reducer = new Reducer(text);
            _store.Dispatch(reducer);
        }
    }
}
