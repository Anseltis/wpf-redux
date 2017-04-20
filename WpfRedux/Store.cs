using System;

namespace WpfRedux
{
    public class Store
    {
        private readonly Reducer _reducer;
        public State State { get; private set; }

        public Store(Reducer reducer, State intitialState)
        {
            _reducer = reducer;
            State = intitialState;
            
        }

        public event EventHandler<ChangeStateArgs> StateChanged;

        public void Dispatch(Action action)
        {
            var oldState = State;
            State = _reducer.Reduce(oldState, action);

            var handler = StateChanged;
            handler?.Invoke(this, new ChangeStateArgs(oldState, State));
        }        
    }
}
