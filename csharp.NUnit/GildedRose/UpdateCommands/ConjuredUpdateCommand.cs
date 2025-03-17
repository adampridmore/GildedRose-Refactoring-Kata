namespace GildedRoseKata.UpdateCommands;

public class ConjuredUpdateCommand(Item item) : ItemUpdateCommand(item)
{
    protected override void UpdateQuality()
    {
        ReduceQuality();
        ReduceQuality();
    }
}