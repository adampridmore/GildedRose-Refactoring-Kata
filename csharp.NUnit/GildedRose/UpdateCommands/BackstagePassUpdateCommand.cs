namespace GildedRoseKata.UpdateCommands;

public class BackstagePassUpdateCommand(Item item2) : ItemUpdateCommand(item2)
{
    protected override void UpdateQuality()
    {
        switch (Item.SellIn)
        {
            case < 0:
                Item.Quality = 0;
                break;
            case < 5:
                IncreaseQuality(3);
                break;
            case < 10:
                IncreaseQuality(2);
                break;
            default:
                IncreaseQuality(1);
                break;
        }
    }
}