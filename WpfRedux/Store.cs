using System;

namespace WpfRedux
{
    public class Store
    {
        public State State { get; private set; } = new State("");

        public void Dispatch(Reducer reducer)
        {
            var oldState = State;
            State = reducer.Reduce(oldState);

            var handler = StateChanged;
            handler?.Invoke(this, new ChangeStateArgs(oldState, State));
        }

        public event EventHandler<ChangeStateArgs> StateChanged;
    }
}
