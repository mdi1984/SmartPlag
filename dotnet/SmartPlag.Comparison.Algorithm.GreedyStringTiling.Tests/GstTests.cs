using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SmartPlag.Comparison.Algorithm.GreedyStringTiling;
using SmartPlag.Comparison.Algorithm.GreedyStringTiling.Model;

namespace SmartPlag.Comparison.GreedyStringTiling.Algorithm.Tests
{
  [TestClass]
  public class GstTests
  {
    [TestMethod]
    public void BasicGstTest()
    {
      var s1 = new Sequence<int>(
        new List<Token<int>>()
        {
          new Token<int>(1 , 1 , 2  ),
          new Token<int>(2 , 2 , 3  ),
          new Token<int>(3 , 3 , 4  ),
          new Token<int>(4 , 4 , 5  ),
          new Token<int>(5 , 5 , 6  ),
          new Token<int>(6 , 6 , 7  ),
          new Token<int>(7 , 7 , 8  ),
          new Token<int>(8 , 8 , 9  ),
          new Token<int>(9 , 9 , 10 ),
          new Token<int>(10, 10, 11 ),
          new Token<int>(11, 11, 12 ),
          new Token<int>(12, 12, 13 ),
        });

      var s2 = new Sequence<int>(
        new List<Token<int>>()
        {
          new Token<int>(1, 1 , 2 ),
          new Token<int>(2, 2 , 3 ),
          new Token<int>(3, 3 , 4 ),
          new Token<int>(1, 4 , 5 ),
          new Token<int>(2, 5 , 6 ),
          new Token<int>(3, 6 , 7 ),
          new Token<int>(8, 7 , 8 ),
          new Token<int>(9, 8 , 9 ),
          new Token<int>(4, 9 , 10),
          new Token<int>(5, 10, 11),
          new Token<int>(6, 11, 12),
          new Token<int>(7, 12, 13),
        });

      var result = GstComparator.Compare(s1, s2, 3);
      Assert.AreEqual(2, result.Count);

      // First match 4,5,6,7 from pattern pos 3 and text pos 8
      Assert.AreEqual(3, result[0].PatternPosition);
      Assert.AreEqual(8, result[0].TextPosition);
      Assert.AreEqual(4, result[0].Length);

      // Second match 1,2,3 from pattern pos 0 and text pos 0
      Assert.AreEqual(0, result[1].PatternPosition);
      Assert.AreEqual(0, result[1].TextPosition);
      Assert.AreEqual(3, result[1].Length);

    }
  }
}
