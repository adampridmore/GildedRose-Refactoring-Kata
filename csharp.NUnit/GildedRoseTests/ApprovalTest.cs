using GildedRoseKata;

using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

using VerifyNUnit;

using NUnit.Framework;

namespace GildedRoseTests;

public class ApprovalTest
{
    [Test]
    public Task ThirtyDays()
    {
        var fakeOutput = new StringBuilder();
        Console.SetOut(new StringWriter(fakeOutput));
        Console.SetIn(new StringReader($"a{Environment.NewLine}"));

        Program.Main(new string[] { "30" });
        var output = fakeOutput.ToString();

        return Verifier.Verify(output);
    }
    
    [Test]
    public Task NormalItemThirtyDays()
    {
        var fakeOutput = new StringBuilder();
        Console.SetOut(new StringWriter(fakeOutput));
        Console.SetIn(new StringReader($"a{Environment.NewLine}"));

        var items = new List<Item>
        {
            new Item { Name = "+5 Dexterity Vest", SellIn = 2, Quality = 5 }
        };
        Program.ProcessItems(items, 30);
        var output = fakeOutput.ToString();

        return Verifier.Verify(output);
    }
    
    [Test]
    public Task AgesBrieItemThirtyDays()
    {
        var fakeOutput = new StringBuilder();
        Console.SetOut(new StringWriter(fakeOutput));
        Console.SetIn(new StringReader($"a{Environment.NewLine}"));

        var items = new List<Item>
        {
            new Item { Name = "Aged Brie", SellIn = 10, Quality = 20 }
        };
        Program.ProcessItems(items, 30);
        var output = fakeOutput.ToString();

        return Verifier.Verify(output);
    }
    
    [Test]
    public Task LegendaryItemThirtyDays()
    {
        var fakeOutput = new StringBuilder();
        Console.SetOut(new StringWriter(fakeOutput));
        Console.SetIn(new StringReader($"a{Environment.NewLine}"));

        var items = new List<Item>
        {
            new Item {Name = "Sulfuras, Hand of Ragnaros", SellIn = 0, Quality = 80},
        };
        Program.ProcessItems(items, 30);
        var output = fakeOutput.ToString();

        return Verifier.Verify(output);
    }
    
    [Test]
    public Task BackstagePassThirtyDays()
    {
        var fakeOutput = new StringBuilder();
        Console.SetOut(new StringWriter(fakeOutput));
        Console.SetIn(new StringReader($"a{Environment.NewLine}"));

        var items = new List<Item>
        {
            new Item {  Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = 15, Quality = 20 },
        };
        Program.ProcessItems(items, 30);
        var output = fakeOutput.ToString();

        return Verifier.Verify(output);
    }
    
    [Test]
    public Task ConjuredItemThirtyDays()
    {
        var fakeOutput = new StringBuilder();
        Console.SetOut(new StringWriter(fakeOutput));
        Console.SetIn(new StringReader($"a{Environment.NewLine}"));

        var items = new List<Item>
        {
            new Item {Name = "Conjured Mana Cake", SellIn = 3, Quality = 6}
        };
        Program.ProcessItems(items, 30);
        var output = fakeOutput.ToString();

        return Verifier.Verify(output);
    }
}
