﻿using Common;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace IdentifiersTable
{
    class ViewModel : INotifyPropertyChanged
    {
        private const int size = 100;
        private readonly HashTable hashTable = new HashTable(size);
        private readonly List<string> sortedList = new List<string>();
        private string addTextBox;
        private string searchTextBox;
        private string pathTextBox;

        public ObservableCollection<Identifier> Identifiers { get; set; } = new ObservableCollection<Identifier>();
        public event PropertyChangedEventHandler PropertyChanged;
        public ICommand AddCommand { get; set; }
        public ICommand SearchCommand { get; set; }
        public ICommand LoadCommand { get; set; }

        public ViewModel()
        {
            AddCommand = new Command(
                                    () => { AddIdentifiers(AddTextBox); },
                                    () => true
                                    );

            SearchCommand = new Command(
                                        () => { SearchIdentifier(SearchTextBox); },
                                        () => true
                                        );

            LoadCommand = new Command(
                                        () => { LoadFromFile(PathTextBox); },
                                        () => true
                                        );
         }

        public string AddTextBox
        {
            get => addTextBox;
            set
            {
                addTextBox = value;
                OnPropertyChanged();
            }
        }

        public string SearchTextBox
        {
            get => searchTextBox;
            set
            {
                searchTextBox = value;
                OnPropertyChanged();
            }
        }

        public string PathTextBox
        {
            get => pathTextBox;
            set
            {
                pathTextBox = value;
                OnPropertyChanged();
            }
        }

        private void AddIdentifiers(string identifiers)
        {
            var splitedIdentifiers = identifiers.Split(',');
            foreach(string stringId in splitedIdentifiers)
            {
                var hashTableIndex = hashTable.Add(stringId);
                var sortedListIndex = AddToList(stringId);
                this.Identifiers
                    .Where((i) => i.SimpleIndex >= sortedListIndex)
                    .ToList()
                    .ForEach((i) => i.SimpleIndex++);
                this.Identifiers.Add(new Identifier(stringId, sortedListIndex, hashTableIndex));
            }

            AddTextBox = string.Empty;
            OnPropertyChanged(nameof(Identifiers));
        }

        private int AddToList(string identifier)
        {
            if (sortedList.Count == size)
            {
                return -1;
            }

            if (sortedList.Count == 0)
            {
                sortedList.Add(identifier);
                return 0;
            }

            int i = 0;
            while (i < sortedList.Count && sortedList[i].CompareTo(identifier) < 0)
            {
                i++;
            }

            if (sortedList[i] == identifier)
            {
                return -1;
            }

            sortedList.Insert(i, identifier);

            return i;
        }

        private void SearchIdentifier(string identifier)
        {
            var indexInSortedList = BinarySearchInSortedList(identifier);
            var indexInHashTable = hashTable.Search(identifier);
        }

        private int BinarySearchInSortedList(string identifier)
        {
            var left = 0;
            var right = sortedList.Count - 1;
            while (left <= right)
            {
                var middle = (left + right) / 2;
                if (sortedList[middle].CompareTo(identifier) < 0)
                {
                    left = middle + 1;
                }
                else if (sortedList[middle].CompareTo(identifier) > 0)
                {
                    right = middle - 1;
                }
                else
                {
                    return middle;
                }
            }
            return -1;
        }

        private void LoadFromFile(string path)
        {
            if (File.Exists(path))
            {
                var text = File.ReadAllText(path);
                AddIdentifiers(text);
            }
        }

        private void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
