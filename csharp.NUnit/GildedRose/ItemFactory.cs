using GildedRoseKata.Items;

namespace GildedRoseKata;

public static class ItemFactory
{
    public static Item CreateItem(string name, int sellIn, int quality)
    {
        Item item = name switch
        {
            "Aged Brie" => new AgedBrie(),
            "Backstage passes to a TAFKAL80ETC concert" => new BackstagePass(),
            "Sulfuras, Hand of Ragnaros" => new Sulfuras(),
            _ => new StandardItem(name)
        };

        item.SellIn = sellIn;
        item.Quality = quality;
        return item;
    }
} 
