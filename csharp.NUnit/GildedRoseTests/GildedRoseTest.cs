using System.Collections.Generic;
using GildedRoseKata;
using NUnit.Framework;

namespace GildedRoseTests;

public class GildedRoseTest
{
    /*
     * +5 Dexterity Vest, 10, 20
       Aged Brie, 2, 0
       Elixir of the Mongoose, 5, 7
       Sulfuras, Hand of Ragnaros, 0, 80
       Sulfuras, Hand of Ragnaros, -1, 80
       Backstage passes to a TAFKAL80ETC concert, 15, 20
       Backstage passes to a TAFKAL80ETC concert, 10, 49
       Backstage passes to a TAFKAL80ETC concert, 5, 49
       Conjured Mana Cake, 3, 6
     */
    [Test]
    public void BasicItem()
    {
        
        var items = new List<Item>
        {
            new Item { Name = "+5 Dexterity Vest", SellIn = 10, Quality = 20 }
        };
        var app = new GildedRose(items);
        app.UpdateQuality();
        Assert.That(items[0].Name, Is.EqualTo("+5 Dexterity Vest"));
        Assert.That(items[0].SellIn, Is.EqualTo(9));
        Assert.That(items[0].Quality, Is.EqualTo(19));
    }

    [Test]
    [Ignore("TODO")]
    public void ConjouredItem()
    {
        //Conjured Mana Cake, 3, 6
        var items = new List<Item>
        {
            new Item { Name = "Conjured Mana Cake", SellIn = 3, Quality = 6 }
        };
        var app = new GildedRose(items);
        app.UpdateQuality();
        Assert.That(items[0].Name, Is.EqualTo("Conjured Mana Cake"));
        Assert.That(items[0].SellIn, Is.EqualTo(2));
        Assert.That(items[0].Quality, Is.EqualTo(4));
    }
}