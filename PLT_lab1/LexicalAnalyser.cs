using System;
using System.Collections.Generic;
using System.Linq;

namespace PLT_lab1
{
    class LexicalAnalyser : ILexicalAnalyser
    {
        private readonly Action<string> log;
        private readonly State initialState = State.H;
        private readonly List<State> finalStates = new List<State> { State.S, State.SP };
        private readonly List<Transition> transitions = new List<Transition>
        {
            new Transition(State.H, State.P, '('),
            new Transition(State.H, State.SP, '('),
            new Transition(State.SP, State.SP, '('),
            new Transition(State.SP, State.P, ')'),
            new Transition(State.S, State.S, '('),
            new Transition(State.S, State.P, ')')
        };

        public LexicalAnalyser(Action<string> log)
        {
            this.log = log;
        }

        public bool AnalyzeWord(string word)
        {
            State currentState = initialState;
            foreach (char symbol in word)
            {
                var transition = transitions.FirstOrDefault((t) => t.FromState == currentState && t.Symbol == symbol);
                if (transition == null)
                {
                    return false;
                }
                currentState = transition.ToState;
            }
            return finalStates.Contains(currentState);
        }

        private void Log(string text)
        {
            log(text);
        }
    }
}
