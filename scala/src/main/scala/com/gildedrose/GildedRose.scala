package com.gildedrose

class GildedRose(val items: Array[Item]) {

  def increaseQualityRules(item: Item){
    if (item.quality < 50) {
      item.quality = item.quality + 1

      if (item.isBackstagePass) {
        if (item.sellIn < 11 && item.quality < 50) {
          item.quality = item.quality + 1
        }

        if (item.sellIn < 6 && item.quality < 50) {
          item.quality = item.quality + 1
        }
      }
    }
  }

  def reduceQualityRules(item: Item){ 
    if (item.quality > 0) {
      if (!item.isLegendary) {
        item.quality = item.quality - 1
      }
    }
  }

  def processItem(item: Item) {
   if (item.isAgedBrie || item.isBackstagePass) {
      increaseQualityRules(item)
    } else {
      reduceQualityRules(item)
    }

    if (!item.isLegendary) {
      item.sellIn = item.sellIn - 1
    }

    if (item.outOfDate) {
      if (item.isAgedBrie) {
        if (item.quality < 50) {
          item.quality = item.quality + 1
        }
      } else if (item.isBackstagePass) {
        item.quality = 0
      } else if (item.quality > 0 && !item.isLegendary) {
        item.quality = item.quality - 1
      }
    }
  }
 
  def updateQuality() {
    items.foreach(processItem)
  }
}
