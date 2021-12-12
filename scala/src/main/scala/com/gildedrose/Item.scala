package com.gildedrose

// TODO: Make imutable
class Item(val name: String, var sellIn: Int, var quality: Int){
  val isLegendary = name.equals("Sulfuras, Hand of Ragnaros")

  def isBackstagePass = name.equals("Backstage passes to a TAFKAL80ETC concert")
  
  def isAgedBrie = name.equals("Aged Brie")

  def outOfDate = sellIn < 0
}
