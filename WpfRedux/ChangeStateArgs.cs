namespace WpfRedux
{
    public class ChangeStateArgs
    {
        public State OldState { get; }
        public State NewState { get; }

        public ChangeStateArgs(State oldState, State newState)
        {
            OldState = oldState;
            NewState = newState;
        }
    }
}
