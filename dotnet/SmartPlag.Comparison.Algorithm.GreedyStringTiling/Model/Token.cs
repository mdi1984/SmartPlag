using System;

namespace SmartPlag.Comparison.Algorithm.GreedyStringTiling.Model
{
  public class Token<T> where T : IComparable
  {
    public Token(T value)
    {
      this.Value = value;
    }

    internal T Value { get; private set; }
    internal bool IsMarked { get; set; }
  }
}