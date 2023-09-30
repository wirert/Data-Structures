namespace _07.Sequence_N_M
{
    internal class Item
    {
        public Item(int value)
        {
            Value = value;
        }

        public Item(int value, Item origin) :this(value)
        {
            Origin = origin;
        }

        public int Value { get; set; }

        public Item Origin { get; set; }
    }
}
