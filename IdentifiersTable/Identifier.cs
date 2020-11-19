namespace IdentifiersTable
{
    class Identifier
    {
        public string Value { get; }
        public int SimpleIndex { get; }
        public int IndexInHashTable { get; }

        public Identifier(string value, int simpleIndex, int indexInHashTable)
        {
            Value = value;
            SimpleIndex = simpleIndex;
            IndexInHashTable = indexInHashTable;
        }
    }
}
