using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Common;

namespace LexicalAnalyser
{
    class ViewModel : INotifyPropertyChanged
    {
        private readonly IAnalyser analyser;
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
                    () => { Run(); }
                );
            analyser = new Analyser( (s) => LogLine(s) );
        }

        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

        public void Run()
        {
            var result = analyser.AnalyzeWord(Word);
            LogLine();
        }

        private void LogLine(string line = null)
        {
            LogText += line;
            logText += Environment.NewLine;
        }
    }
}
