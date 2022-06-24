namespace CodeBase.Logic.Loot
{
    public class Loot
    {
        public LootType LootType;
        public int Value;

        public Loot(LootType lootType, int value)
        {
            LootType = lootType;
            Value = value;
        }
    }
}