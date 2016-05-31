using System;
using System.Collections.Generic;

namespace SmartPlag.Comparison.Algorithm.GreedyStringTiling.Model
{
  public class Sequence<T> where T : IComparable
  {
    public Sequence(IList<Token<T>> tokens)
    {
      this.Tokens = tokens;
    }

    internal IList<Token<T>> Tokens { get; private set; } 
  }
}