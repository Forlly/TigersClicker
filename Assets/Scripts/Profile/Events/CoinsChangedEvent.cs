namespace Project
{
    public struct CoinsChangedEvent
    {
        public readonly Coins Coins;

        public CoinsChangedEvent(Coins coins)
        {
            Coins = coins;
        }
    }
}