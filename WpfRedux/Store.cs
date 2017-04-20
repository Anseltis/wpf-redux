using System;
using System.Collections.Concurrent;

namespace WpfRedux
{
    public class Store
    {
        private readonly Reducer _reducer;
        private readonly ConcurrentStack<State> _stack;
        public State State
        {
            get
            {
                State state;
                _stack.TryPeek(out state);
                return state;
            }
        }

        public Store(Reducer reducer, State initialState)
        {
            _reducer = reducer;
            _stack = new ConcurrentStack<State>();
            _stack.Push(initialState);
        }

        public event EventHandler<ChangeStateArgs> StateChanged;

        public void Dispatch(Action action)
        {
            Update(_reducer.Reduce(State, action));
        }

        private void Update(State state)
        {
            var oldState = State;
            _stack.Push(state);

            var handler = StateChanged;
            handler?.Invoke(this, new ChangeStateArgs(oldState, State));
        }

        public void Undo()
        {
            if(_stack.Count > 2)
            {
                var oldState = State;
                State state;
                _stack.TryPop(out state);
                var handler = StateChanged;
                handler?.Invoke(this, new ChangeStateArgs(oldState, State));
            }

            
        }
    }
}
