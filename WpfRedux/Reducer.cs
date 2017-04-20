namespace WpfRedux
{
    public class Reducer
    {
        public State Reduce(State state, Action action)
        {
            return new State(action.Text);
        }
    }
}
