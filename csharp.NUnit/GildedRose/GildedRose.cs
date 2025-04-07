using System;
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
        if (item.Quality > 0)
        {
            item.Quality--;
        }
    }

    public static void IncreaseQuality(this Item item, int amount = 1)
    {
        item.Quality+=amount;
        
        if (item.Quality > 50)
        {
            item.Quality = 50;
        }
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
    public virtual void UpdateQuality(Item item)
    {
        item.ReduceQuality();

        if (item.SellIn < 0)
        {
            item.ReduceQuality();
        }
    }

    public static ItemUpdateCommand Create(Item item)
    {
        if (item.IsNormalItem())
        {
            return new ItemUpdateCommand();
        }

        if (item.IsLegendary())
        {
            return new LegendaryUpdateCommand();
        }

        if (item.IsAgedBrie())
        {
            return new AgedBrieUpdateCommand();
        }

        if (item.IsBackstagePass())
        {
            return new BackstagePassUpdateCommand();
        }

        throw new ApplicationException($"Unknown item type: {item.Name}");
    }
}

public class LegendaryUpdateCommand : ItemUpdateCommand
{
    public override void UpdateQuality(Item item)
    {

    }
}

public class AgedBrieUpdateCommand : ItemUpdateCommand
{
    public override void UpdateQuality(Item item)
    {
        item.IncreaseQuality();

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
    public override void UpdateQuality(Item item)
    {
        switch (item.SellIn)
        {
            case < 0:
                item.Quality = 0;
                break;
            case < 5:
                item.IncreaseQuality(3);
                break;
            case < 10:
                item.IncreaseQuality(2);
                break;
            default:
                item.IncreaseQuality(1);
                break;
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
        item.ReduceSellIn();
        
        var command = ItemUpdateCommand.Create(item);
        command.UpdateQuality(item);
    }
}