using System.Collections.Generic;
using System.Data;

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

    public static bool IsLegendary(this Item item)
    {
        return item.Name == "Sulfuras, Hand of Ragnaros";
    }

    public static bool IsNormalItem(this Item item)
    {
        if (item.IsAgedBrie() || item.IsBackstagePass() || item.IsLegendary())
        {
            return false;
        }

        return true;
    } 

    public static void ReduceQuality(this Item item)
    {
        if (item.IsLegendary())
        {
            return;
        }
        
        if (item.Quality > 0)
        {
            item.Quality--;
        }
    }

    public static void IncreaseQuality(this Item item)
    {
        if (item.Quality >= 50)
        {
            return;
        }
        
        item.Quality++;
    }
    
    public static void ReduceSellIn(this Item item)
    {
        if (item.IsLegendary())
        {
            return;
        }

        item.SellIn--;
    }
}

public class ItemUpdaterCommand
{
    public void Update(Item item)
    {
        item.ReduceQuality();
        item.ReduceSellIn();

        if (item.SellIn < 0)
        {
            item.ReduceQuality();
        }
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
        if (item.IsNormalItem())
        {
            var command = new ItemUpdaterCommand();
            command.Update(item);
            return;
        }
        
        if (item.IsAgedBrie() || item.IsBackstagePass())
        {
            if (item.Quality < 50)
            {
                item.IncreaseQuality();

                if (item.IsBackstagePass())
                {
                    if (item.SellIn < 11)
                    {
                        item.IncreaseQuality();
                    }

                    if (item.SellIn < 6)
                    {
                        item.IncreaseQuality();
                    }
                }
            }
        }
        else
        {
            item.ReduceQuality();
        }

        item.ReduceSellIn();
        
        if (item.SellIn < 0)
        {
            if (item.IsAgedBrie())
            {
                item.IncreaseQuality();
            }
            else if (item.IsBackstagePass())
            {
                item.Quality = 0;
            }
            else
            {
                item.ReduceQuality();
            }
        }
    }
}