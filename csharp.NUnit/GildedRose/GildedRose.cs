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

public class ItemUpdateCommand
{
    public virtual  void Update(Item item)
    {
        item.ReduceQuality();
        item.ReduceSellIn();

        if (item.SellIn < 0)
        {
            item.ReduceQuality();
        }
    }
}

public class LegendaryUpdateCommand : ItemUpdateCommand
{
    public override void Update(Item item)
    {
        item.ReduceQuality();
        item.ReduceSellIn();

        if (item.SellIn < 0)
        {
            item.ReduceQuality();
        }
    }
}

public class AgedBrieUpdateCommand : ItemUpdateCommand
{
    public override void Update(Item item)
    {
        item.IncreaseQuality();
        item.ReduceSellIn();
        
        // TODO: This is a bug, but it's how it works...
        // It's quality increases twice as fast when it's sellIn <0
        if (item.SellIn < 0)
        {
            item.IncreaseQuality();
        }
    }
}

public class BackstagePassUpdateCommand : ItemUpdateCommand
{
    public override void Update(Item item)
    {
        item.ReduceSellIn();

        if (item.SellIn < 0)
        {
            item.Quality = 0;
        }
        else if (item.SellIn < 5)
        {
            item.IncreaseQuality();
            item.IncreaseQuality();
            item.IncreaseQuality();
        }
        else if (item.SellIn < 10)
        {
            item.IncreaseQuality();
            item.IncreaseQuality();
        }
        else
        {
            item.IncreaseQuality();
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
            var command = new ItemUpdateCommand();
            command.Update(item);
            return;
        }

        if (item.IsLegendary())
        {
            var command = new LegendaryUpdateCommand();
            command.Update(item);
            return;
        }
        
        if (item.IsAgedBrie())
        {
            var command = new AgedBrieUpdateCommand();
            command.Update(item);
            return;
        }

        if (item.IsBackstagePass())
        {
            var command = new BackstagePassUpdateCommand();
            command.Update(item);
            return;
        }
    }
}