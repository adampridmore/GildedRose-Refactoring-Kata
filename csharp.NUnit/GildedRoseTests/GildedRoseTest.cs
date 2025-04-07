using System.Collections.Generic;
using GildedRoseKata;
using NUnit.Framework;

namespace GildedRoseTests;

public class GildedRoseTest
{
    [Test]
    [Ignore("WIP")]
    public void Foo()
    {
        var items = new List<Item> { ItemFactory.CreateItem("foo", 0,0) };
        var app = new GildedRose(items);
        app.UpdateQuality();
        Assert.That(items[0].Name, Is.EqualTo("fixme"));
    }
}
