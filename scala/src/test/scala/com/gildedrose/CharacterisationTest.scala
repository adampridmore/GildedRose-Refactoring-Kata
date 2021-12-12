package com.gildedrose

import org.scalatest.flatspec.AnyFlatSpec
import org.scalatest.matchers.should.Matchers
import org.scalatest.AppendedClues

import scala.io.Source
import scala.collection.mutable.ListBuffer
import java.io.FileWriter
import java.io.File
import java.io.BufferedWriter
import java.io.PrintWriter

class CharacterisationTest extends AnyFlatSpec with Matchers with AppendedClues {
  it should "foo" in {
    val actualAudit = ListBuffer[String]()
  
    def audit(message: String = "") : Unit = actualAudit += message

    val expectedAudit : List[String] = Source.fromResource("expected-output.txt").getLines.toList

    val items = Array[Item](
      new Item("+5 Dexterity Vest", 10, 20),
      new Item("Aged Brie", 2, 0),
      new Item("Elixir of the Mongoose", 5, 7),
      new Item("Sulfuras, Hand of Ragnaros", 0, 80),
      new Item("Sulfuras, Hand of Ragnaros", -1, 80),
      new Item("Backstage passes to a TAFKAL80ETC concert", 15, 20),
      new Item("Backstage passes to a TAFKAL80ETC concert", 10, 49),
      new Item("Backstage passes to a TAFKAL80ETC concert", 5, 49),
      // this conjured item does not work properly yet
      new Item("Conjured Mana Cake", 3, 6)
    )
    val app = new GildedRose(items) 
    val days = 20
    for (i <- 0 until days) {
      audit("-------- day " + i + " --------")
      audit("name, sellIn, quality")
      for (item <- items) {
        audit(item.name + ", " + item.sellIn + ", " + item.quality)
      }
      audit()
      app.updateQuality()
    }

    val pw = new PrintWriter(new File("./target/actual-output.txt"))
    actualAudit.foreach(pw.println)
    pw.close()

    actualAudit.zipWithIndex.foreach {
      case (expectedLog, lineNumber)=> println(s"$lineNumber: $expectedLog")
    }

    expectedAudit.zipWithIndex.zip(actualAudit)
      .foreach{case ((expectedLine, lineNumber), actualLine) => {
        expectedLine.shouldBe(actualLine) withClue (s"on line: $lineNumber")
      }}

    actualAudit.length shouldBe expectedAudit.length
  }
}
