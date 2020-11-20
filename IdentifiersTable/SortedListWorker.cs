using System;
using System.Collections.Generic;

namespace IdentifiersTable
{
    class SortedListWorker
    {
        private readonly List<string> sortedList = new List<string>();
        private readonly Action<string> log;
        private int size;

        public SortedListWorker(int size, Action<string> log)
        {
            this.size = size;
            this.log = log;
        }

        public int Add(string identifier)
        {
            if (sortedList.Count == size)
            {
                log("Sorted list is overflowded");
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

            if (i < sortedList.Count && sortedList[i] == identifier)
            {
                log("Identifier already exists in sorted list");
                return -1;
            }

            sortedList.Insert(i, identifier);

            return i;
        }

        public int BinarySearch(string identifier)
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
            log($"Identifier {identifier} is not found in sorted list");
            return -1;
        }
    }
}
