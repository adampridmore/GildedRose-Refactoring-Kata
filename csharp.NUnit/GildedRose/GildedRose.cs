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

public abstract class ItemUpdateCommand(Item item)
{
    public abstract void UpdateQuality();
    
    public static ItemUpdateCommand Create(Item item)
    {
        if (item.IsNormalItem())
        {
            return new NormalItemUpdateCommand(item);
        }

        if (item.IsLegendary())
        {
            return new LegendaryUpdateCommand(item);
        }

        if (item.IsAgedBrie())
        {
            return new AgedBrieUpdateCommand(item);
        }

        if (item.IsBackstagePass())
        {
            return new BackstagePassUpdateCommand(item);
        }

        throw new ApplicationException($"Unknown item type: {item.Name}");
    }
    
    protected Item Item { get; } = item;

    public void Update()
    {
        item.ReduceSellIn();
        
        UpdateQuality();
    }
}

public class NormalItemUpdateCommand(Item item) : ItemUpdateCommand(item)
{
    public override void UpdateQuality()
    {
        Item.ReduceQuality();

        if (Item.SellIn < 0)
        {
            Item.ReduceQuality();
        }
    }
  
}

public class LegendaryUpdateCommand(Item item) : ItemUpdateCommand(item)
{
    public override void UpdateQuality()
    {

    }
}

public class AgedBrieUpdateCommand(Item item) : ItemUpdateCommand(item)
{
    public override void UpdateQuality()
    {
        Item.IncreaseQuality();

        // This is a bug, but it's how it works...
        // It's quality increases twice as fast when it's sellIn < 0
        if (Item.SellIn < 0)
        {
            Item.IncreaseQuality();
        }
    }
}

public class BackstagePassUpdateCommand(Item item2) : ItemUpdateCommand(item2)
{
    public override void UpdateQuality()
    {
        switch (Item.SellIn)
        {
            case < 0:
                Item.Quality = 0;
                break;
            case < 5:
                Item.IncreaseQuality(3);
                break;
            case < 10:
                Item.IncreaseQuality(2);
                break;
            default:
                Item.IncreaseQuality(1);
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
            ItemUpdateCommand
                .Create(item)
                .Update();
        }
    }
}