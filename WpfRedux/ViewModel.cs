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
        public ICommand UndoCommand { get; }

        public ViewModel(Store store)
        {
            _store = store;
            UpdateTextCommand = new RelayActionCommand<string, Action>(text => new Action(text), _store.Dispatch);
            UndoCommand = new RelayCommand(parameter => _store.Undo());
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
    }
}
