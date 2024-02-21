namespace Project
{
    public struct MeatChangedEvent
    {
        public readonly Meat Meat;

        public MeatChangedEvent(Meat meat)
        {
            Meat = meat;
        }
    }
}