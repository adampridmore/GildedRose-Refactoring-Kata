namespace GildedRoseKata.Items;

public class Sulfuras : Item
{
    public Sulfuras()
    {
        Name = "Sulfuras, Hand of Ragnaros";
        Quality = 80;
    }

    public override void UpdateQuality()
    {
        // Sulfuras never changes
    }
} 
