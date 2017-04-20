﻿using System.ComponentModel;
using System.Windows.Input;

namespace WpfRedux
{
    public class ViewModel : INotifyPropertyChanged
    {
        private Store _store;
        private Reducer _reducer;

        public event PropertyChangedEventHandler PropertyChanged;        
        public string Text => _store.State.Text;
        public ICommand UpdateTextCommand { get; }

        public ViewModel(Store store)
        {
            _store = store;
            _reducer = new Reducer();
            UpdateTextCommand = new RelayActionCommand<string, Action>(text => new Action(text), _store.Dispatch);
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
