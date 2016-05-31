using System;
using System.Collections.Generic;
using System.Linq;
using SmartPlag.Comparison.Algorithm.GreedyStringTiling.Model;

namespace SmartPlag.Comparison.Algorithm.GreedyStringTiling
{
  public static class GstComparator
  {
    public static List<Match> Compare<T>(Sequence<T> pattern, Sequence<T> text, int mml) where T : IComparable
    {
      var tiles = new List<Match>();
      var maxMatch = mml;

      do
      {
        maxMatch = mml;
        var matches = new List<Match>();
        for (var a = 0; a < pattern.Tokens.Count; a++)
        {
          for (var b = 0; b < text.Tokens.Count; b++)
          {
            var j = 0;
            while (!pattern.Tokens[a + j].IsMarked && !text.Tokens[b + j].IsMarked &&
                   pattern.Tokens[a + j].Value.Equals(text.Tokens[b + j].Value))
            {
              j++;
              if (a + j >= pattern.Tokens.Count || b + j >= text.Tokens.Count)
                break;
            }

            if (j == maxMatch)
            {
              // TODO: add match only if no other match overlaps1
              if (!matches.Any(m => (m.PatternPosition <= a && m.PatternPosition + m.Length - 1 >= a + j - 1) || 
                    (m.TextPosition <= b && m.TextPosition + m.Length - 1 >= b + j - 1)))
              {
                matches.Add(new Match(a, b, j));
              }
            }
            else if (j > maxMatch)
            {
              // clear matches and add this one - start again you greedy bastard
              matches.Clear();
              matches.Add(new Match(a, b, j));
              maxMatch = j;
            }
          }
        }

        foreach (var item in matches)
        {
          for (var a = 0; a < maxMatch - 1; a++)
          {
            pattern.Tokens[item.PatternPosition + a].IsMarked = true;
            text.Tokens[item.TextPosition + a].IsMarked = true;
          }

          tiles.Add(item);

        }
      } while (maxMatch > mml);

      return tiles;
    }
  }
}