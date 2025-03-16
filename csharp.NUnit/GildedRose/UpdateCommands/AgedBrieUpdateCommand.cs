namespace GildedRoseKata.UpdateCommands;

public class AgedBrieUpdateCommand(Item item) : ItemUpdateCommand(item)
{
    protected override void UpdateQuality()
    {
        IncreaseQuality();

        // This is a bug, but it's how it works...
        // It's quality increases twice as fast when it's sellIn < 0
        if (Item.SellIn < 0)
        {
            IncreaseQuality();
        }
    }
}