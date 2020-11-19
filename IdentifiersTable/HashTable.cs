﻿using System;

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

        public int Add(string value)
        {
            var index = Hash(value);
            if (table[index] == null)
            {
                table[index] = value;
                return index;
            }

            if (table[index] == value)
            {
                return -1;
            }

            for (int i = 1; ; i++)
            {
                int currentHash = (index + i) % size;
                if (table[currentHash] == null)
                {
                    table[currentHash] = value;
                    return currentHash;
                }

                if (table[currentHash] == table[index])
                {
                    return -1;
                }
            }
        }

        public int Search(string value)
        {
            int hashCode = Hash(value);

            if (table[hashCode] == value)
            {
                return hashCode;
            }

            for (int i = 1; ; i++)
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
