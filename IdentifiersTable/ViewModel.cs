using Common;
using System.Windows.Input;
using System.Collections.ObjectModel;

namespace IdentifiersTable
{
    class ViewModel
    {
        private const int size = 100;
        public ObservableCollection<Identifier> Identifiers { get; set; } = new ObservableCollection<Identifier>();
        private HashTable hashTable = new HashTable(size);

        public ICommand AddCommand { get; set; }

        public string AddTextBox { get; set; }

        public ViewModel()
        {
            AddCommand = new Command(
                                    () => { Add(AddTextBox); },
                                    () => true
                                    );
        }

        private void Add(string identifiers)
        {
            var splitedIdentifiers = identifiers.Split(',');
            foreach(string stringId in splitedIdentifiers)
            {
                var hashTableIndex = hashTable.Add(stringId);
                this.Identifiers.Add(new Identifier(stringId, 0, hashTableIndex));
            }
        }
    }
}
