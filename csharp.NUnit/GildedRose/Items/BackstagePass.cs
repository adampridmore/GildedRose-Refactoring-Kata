namespace GildedRoseKata.Items;

public class BackstagePass : Item
{
    public BackstagePass()
    {
        Name = "Backstage passes to a TAFKAL80ETC concert";
    }

    public override void UpdateQuality()
    {
        if (Quality < 50)
        {
            IncreaseQuality();
            
            if (SellIn <= 10 && Quality < 50)
            {
                IncreaseQuality();
            }
            
            if (SellIn <= 5 && Quality < 50)
            {
                IncreaseQuality();
            }
        }
        
        SellIn--;
        
        if (SellIn < 0)
        {
            Quality = 0;
        }
    }
} 
