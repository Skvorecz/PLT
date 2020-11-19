using Common;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace IdentifiersTable
{
    class ViewModel : INotifyPropertyChanged
    {
        private const int size = 100;
        private HashTable hashTable = new HashTable(size);
        private string addTextBox;

        public ObservableCollection<Identifier> Identifiers { get; set; } = new ObservableCollection<Identifier>();
        public event PropertyChangedEventHandler PropertyChanged;
        public ICommand AddCommand { get; set; }

        public ViewModel()
        {
            AddCommand = new Command(
                                    () => { Add(AddTextBox); },
                                    () => true
                                    );
        }

        public string AddTextBox
        {
            get
            {
                return addTextBox;
            }
            set
            {
                addTextBox = value;
                OnPropertyChanged();
            }
        }

        private void Add(string identifiers)
        {
            var splitedIdentifiers = identifiers.Split(',');
            foreach(string stringId in splitedIdentifiers)
            {
                var hashTableIndex = hashTable.Add(stringId);
                this.Identifiers.Add(new Identifier(stringId, 0, hashTableIndex));
            }
            AddTextBox = string.Empty;
        }

        private void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
