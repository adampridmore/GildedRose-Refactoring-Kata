using System;

namespace GildedRoseKata;

public abstract class Item
{
    public string Name { get; set; }
    public int SellIn { get; set; }
    public int Quality { get; set; }

    public virtual void UpdateQuality()
    {
        if (Quality > 0)
        {
            Quality--;
        }
        SellIn--;
        if (SellIn < 0 && Quality > 0)
        {
            Quality--;
        }
    }

    protected void IncreaseQuality(int amount = 1)
    {
        Quality = Math.Min(50, Quality + amount);
    }

    protected void DecreaseQuality(int amount = 1)
    {
        Quality = Math.Max(0, Quality - amount);
    }
}
