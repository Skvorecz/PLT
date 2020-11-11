using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace PLT_lab1
{
    class ViewModel : INotifyPropertyChanged
    {
        private string log;

        public event PropertyChangedEventHandler PropertyChanged;

        public ICommand ButtonCommand { get; }

        public string Word { get; set; }
        public string Log 
        {
            get
            {
                return log;
            }
            set
            {
                log = value;
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
    }
}
