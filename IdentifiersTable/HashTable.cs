using System;

namespace IdentifiersTable
{
    class HashTable
    {
        readonly int size;
        readonly string[] table;

        public HashTable(int size)
        {
            this.size = size;
            table = new string[size];
        }

        public void Add(string value)
        {
            table[Hash(value)] = value;
        }

        public int Search(string value)
        {
            int hashCode = Hash(value);

            for (int i = 0; ; i++)
            {
                int currentHash = (hashCode + i) % size;
                if (table[currentHash] == null
                    || table[currentHash] == table[hashCode])
                {
                    return -1;
                }

                if (table[currentHash] == value)
                {
                    return currentHash;
                }
            }
        }

        private int Hash(string value)
        {
            return Math.Abs(value.GetHashCode()) % size;
        }
    }
}
