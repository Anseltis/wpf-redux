namespace WpfRedux
{
    public class Reducer
    {
        private string _text;
        public Reducer(string text)
        {
            _text = text;
        }
        public State Reduce(State state)
        {
            return new State(_text);
        }
    }
}
