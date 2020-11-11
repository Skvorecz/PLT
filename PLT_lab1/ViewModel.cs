using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace PLT_lab1
{
    class ViewModel : INotifyPropertyChanged
    {
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
        }

        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

        public void Run()
        {

        }

        private void LogLine(string line)
        {
            LogText += line;
            logText += Environment.NewLine;
        }
    }
}
