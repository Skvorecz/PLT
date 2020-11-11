using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace PLT_lab1
{
    class ViewModel : INotifyPropertyChanged
    {
        private readonly ILexicalAnalyser lexicalAnalyser;
        private string logText;

        public event PropertyChangedEventHandler PropertyChanged;
        public ICommand ButtonCommand { get; }
        public string Word { get; set; }
        public string LogText
        {
            get
            {
                return logText;
            }
            set
            {
                logText = value;
                OnPropertyChanged();                
            }
        }

        public ViewModel()
        {
            ButtonCommand = new Command
                (
                    () => { Run(); },
                    () => true
                );
            lexicalAnalyser = new LexicalAnalyser( (s) => LogLine(s) );
        }

        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

        public void Run()
        {
            var result = lexicalAnalyser.AnalyzeWord(Word);
            LogLine();
        }

        private void LogLine(string line = null)
        {
            LogText += line;
            logText += Environment.NewLine;
        }
    }
}
