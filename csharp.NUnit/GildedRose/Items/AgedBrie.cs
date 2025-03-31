namespace GildedRoseKata.Items;

public class AgedBrie : Item
{
    public AgedBrie()
    {
        Name = "Aged Brie";
    }

    public override void UpdateQuality()
    {
        if (Quality < 50)
        {
            IncreaseQuality();
        }
        SellIn--;
        if (SellIn < 0 && Quality < 50)
        {
            IncreaseQuality();
        }
    }
} 
