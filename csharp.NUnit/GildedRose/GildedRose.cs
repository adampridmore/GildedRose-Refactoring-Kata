using System.Collections.Generic;

namespace GildedRoseKata;

public static class ItemExtension
{
    public static bool IsAgedBrie(this Item item)
    {
        return item.Name == "Aged Brie";
    }

    public static bool IsBackstagePass(this Item item)
    {
        return item.Name == "Backstage passes to a TAFKAL80ETC concert";
    }

    public static bool IsLegendaryItem(this Item item)
    {
        return item.Name == "Sulfuras, Hand of Ragnaros";
    }
}


public class GildedRose(IList<Item> items)
{
    public void UpdateQuality()
    {
        foreach (var item in items)
        {
            UpdateItem(item);
        }
    }

    private static void UpdateItem(Item item)
    {
        if (item.IsAgedBrie() || item.IsBackstagePass())
        {
            if (item.Quality < 50)
            {
                item.Quality = item.Quality + 1;

                if (item.IsBackstagePass())
                {
                    if (item.SellIn < 11)
                    {
                        if (item.Quality < 50)
                        {
                            item.Quality = item.Quality + 1;
                        }
                    }

                    if (item.SellIn < 6)
                    {
                        if (item.Quality < 50)
                        {
                            item.Quality = item.Quality + 1;
                        }
                    }
                }
            }
        }
        else
        {
            if (item.Quality > 0)
            {
                if (!item.IsLegendaryItem())
                {
                    item.Quality = item.Quality - 1;
                }
            }
        }

        if (!item.IsLegendaryItem())
        {
            item.SellIn = item.SellIn - 1;
        }

        if (item.SellIn < 0)
        {
            if (!item.IsAgedBrie())
            {
                if (item.IsBackstagePass())
                {
                    item.Quality = item.Quality - item.Quality;
                }
                else
                {
                    if (item.Quality > 0)
                    {
                        if (!item.IsLegendaryItem())
                        {
                            item.Quality = item.Quality - 1;
                        }
                    }
                }
            }
            else
            {
                if (item.Quality < 50)
                {
                    item.Quality = item.Quality + 1;
                }
            }
        }
    }
}