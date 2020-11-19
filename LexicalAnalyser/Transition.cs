namespace LexicalAnalyser
{
    class Transition
    {
        public State FromState { get; }
        public State ToState { get; }
        public char Symbol { get; }

        public Transition(State fromState, State toState, char symbol)
        {
            FromState = fromState;
            ToState = toState;
            Symbol = symbol;
        }

        public override string ToString()
        {
            return $"f( {FromState}, {Symbol} ) = {ToState}";
        }
    }
}
