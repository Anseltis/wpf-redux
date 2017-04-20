using System.ComponentModel;
using System.Windows.Input;

namespace WpfRedux
{
    public class ViewModel : INotifyPropertyChanged
    {
        private Store _store;

        public event PropertyChangedEventHandler PropertyChanged;        
        public string Text => _store.State.Text;
        public ICommand UpdateTextCommand { get; }

        public ViewModel(Store store)
        {
            _store = store;
            UpdateTextCommand = new RelayCommand<string>(UpdateText);
            store.StateChanged += ChangeState;            
        }                

        private void OnPropertyChanged(string propertyName)
        {
            var handler = PropertyChanged;
            handler?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        
        public void ChangeState(object sender, ChangeStateArgs args)
        {
            if(args?.OldState?.Text != args?.NewState?.Text)
            {
                OnPropertyChanged(nameof(Text));
            }
        }

        private void UpdateText(string text)
        {
            var reducer = new Reducer(text);
            _store.Dispatch(reducer);
        }
    }
}
