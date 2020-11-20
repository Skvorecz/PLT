using Common;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using System.Linq;
using System.IO;

namespace IdentifiersTable
{
    class ViewModel : INotifyPropertyChanged
    {
        private const int size = 100;
        private readonly HashTable hashTable;
        private readonly SortedListWorker sortedListWorker;
        private Action<string> LogLine;
        private string addText;
        private string searchText;
        private string searchResult;
        private string pathText;
        private string log;

        public ObservableCollection<Identifier> Identifiers { get; set; } = new ObservableCollection<Identifier>();
        public event PropertyChangedEventHandler PropertyChanged;
        public ICommand AddCommand { get; set; }
        public ICommand SearchCommand { get; set; }
        public ICommand LoadCommand { get; set; }

        public ViewModel()
        {
            InitializeCommands();
            LogLine = (string s) => Log += (s + Environment.NewLine);
            hashTable = new HashTable(size, LogLine);
            sortedListWorker = new SortedListWorker(size, LogLine);
        }

        public string AddText
        {
            get => addText;
            set
            {
                addText = value;
                OnPropertyChanged();
            }
        }

        public string SearchText
        {
            get => searchText;
            set
            {
                searchText = value;
                OnPropertyChanged();
            }
        }

        public string SearchResult
        {
            get => searchResult;
            private set
            {
                searchResult = value;
                OnPropertyChanged();
            }
        }

        public string PathText
        {
            get => pathText;
            set
            {
                pathText = value;
                OnPropertyChanged();
            }
        }

        public string Log
        {
            get => log;
            private set
            {
                log = value;
                OnPropertyChanged();
            }
        }

        private void InitializeCommands()
        {
            AddCommand = new Command(
                                        () => { AddIdentifiers(AddText); }
                                        );

            SearchCommand = new Command(
                                        () => { SearchIdentifier(SearchText); }
                                        );

            LoadCommand = new Command(
                                        () => { LoadFromFile(PathText); }
                                        );
        }

        private void AddIdentifiers(string identifiers)
        {
            var splitedIdentifiers = identifiers.Split(',');
            foreach (string stringId in splitedIdentifiers)
            {
                var hashTableIndex = hashTable.Add(stringId);
                var sortedListIndex = sortedListWorker.Add(stringId);

                if (hashTableIndex >= 0 && sortedListIndex >= 0)
                {
                    this.Identifiers
                        .Where((i) => i.SimpleIndex >= sortedListIndex)
                        .ToList()
                        .ForEach((i) => i.SimpleIndex++);
                    this.Identifiers.Add(new Identifier(stringId, sortedListIndex, hashTableIndex));
                }
            }

            AddText = string.Empty;
            OnPropertyChanged(nameof(Identifiers));
        }

        private void SearchIdentifier(string identifier)
        {
            var indexInSortedList = sortedListWorker.BinarySearch(identifier);
            var indexInHashTable = hashTable.Search(identifier);

            if (indexInSortedList >= 0 && indexInHashTable >= 0)
            {
                SearchResult = $"Identifier: {identifier}\n" +
                    $"Index in sorted list: {indexInSortedList}\n" +
                    $"Index in hash table: {indexInHashTable}";
                return;
            }

            SearchResult = "Search failed";            
        }

        private void LoadFromFile(string path)
        {
            if (File.Exists(path) && Path.GetExtension(path) == ".txt")
            {
                var text = File.ReadAllText(path);
                AddIdentifiers(text);
            }
            else
            {
                LogLine("Load failed");
            }
        }

        private void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
