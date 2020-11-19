namespace IdentifiersTable
{
    class Identifier
    {
        public string value { get; }
        public int simpleIndex { get; }
        public int indexInHashTable { get; }

        public Identifier(string value, int simpleIndex, int indexInHashTable)
        {
            this.value = value;
            this.simpleIndex = simpleIndex;
            this.indexInHashTable = indexInHashTable;
        }
    }
}
