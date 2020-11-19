using System;
using System.Collections.Generic;
using System.Linq;

namespace LexicalAnalyser
{
    class Analyser : IAnalyser
    {
        private readonly Action<string> log;
        private readonly State initialState = State.H;
        private readonly List<State> finalStates = new List<State> { State.S, State.SP };
        private readonly List<Transition> transitions = new List<Transition>
        {
            new Transition(State.H, State.P, '('),
            new Transition(State.P, State.SP, '('),
            new Transition(State.SP, State.SP, '('),
            new Transition(State.SP, State.P, ')'),
            new Transition(State.S, State.S, '('),
            new Transition(State.S, State.P, ')')
        };

        public Analyser(Action<string> log)
        {
            this.log = log;
        }

        public bool AnalyzeWord(string word)
        {
            log($"Starting analysis for word \"{word}\"");
            State currentState = initialState;
            log($"Current state is {currentState}");
            foreach (char symbol in word)
            {
                log($"Next symbol is {symbol}");
                var transition = transitions.FirstOrDefault((t) =>
                                                            t.FromState == currentState
                                                            && t.Symbol == symbol);
                if (transition == null)
                {
                    log($"Transition f( {currentState}, {symbol} not found");
                    return false;
                }
                log(transition.ToString());
                currentState = transition.ToState;
            }
            return finalStates.Contains(currentState);
        }
    }
}
