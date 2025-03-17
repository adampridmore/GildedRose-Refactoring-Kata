namespace GildedRoseKata.UpdateCommands;

public class NormalItemUpdateCommand(Item item) : ItemUpdateCommand(item)
{
    protected override void UpdateQuality()
    {
        ReduceQuality();

        if (Item.SellIn < 0)
        {
            ReduceQuality();
        }
    }
}