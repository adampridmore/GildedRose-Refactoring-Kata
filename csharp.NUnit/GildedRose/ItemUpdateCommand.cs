using GildedRoseKata.UpdateCommands;

namespace GildedRoseKata;

public abstract class ItemUpdateCommand(Item item)
{
    public static ItemUpdateCommand CreateCommand(Item item)
    {
        return item.Name switch
        {
            "Aged Brie" => new AgedBrieUpdateCommand(item),
            "Backstage passes to a TAFKAL80ETC concert" => new BackstagePassUpdateCommand(item),
            "Sulfuras, Hand of Ragnaros" => new LegendaryUpdateCommand(item),
            "Conjured Mana Cake" => new ConjuredUpdateCommand(item),
            _ => new NormalItemUpdateCommand(item)
        };
    }
    protected abstract void UpdateQuality();

    protected virtual void UpdateSellIn()
    {
        ReduceSellIn();
    }
    
    protected Item Item { get; } = item;

    public void Update()
    {
        UpdateSellIn();
        UpdateQuality();
    }
    
    protected void ReduceQuality()
    {
        if (Item.Quality > 0)
        {
            Item.Quality--;
        }
    }
    
    protected void IncreaseQuality(int amount = 1)
    {
        Item.Quality+=amount;
        
        if (Item.Quality > 50)
        {
            Item.Quality = 50;
        }
    }
    
    private void ReduceSellIn()
    {
        Item.SellIn--;
    }
}