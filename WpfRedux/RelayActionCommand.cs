using System;
using System.Windows.Input;
using WpfRedux;

namespace WpfRedux
{
    class RelayActionCommand<T, TAction> : ICommand
    {
        private readonly ICommand _relayCommand;
        public event EventHandler CanExecuteChanged
        {
            add { _relayCommand.CanExecuteChanged += value; }
            remove { _relayCommand.CanExecuteChanged -= value; }
        }

        public RelayActionCommand(Func<T, TAction> actionFactory, Action<TAction> dispatch)
        {
            _relayCommand = new RelayCommand(parameter => dispatch(actionFactory((T)parameter)));
        }

        public bool CanExecute(object parameter)
        {
            return _relayCommand.CanExecute(parameter);
        }

        public void Execute(object parameter)
        {
            _relayCommand.Execute(parameter);
        }
    }
}
