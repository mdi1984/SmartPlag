using System;

namespace SmartPlag.Comparison.Algorithm.GreedyStringTiling.Model
{
  public class Token<T> where T : IComparable
  {
    public Token(T value, int from, int to)
    {
      this.Value = value;
      this.From = from;
      this.To = to;
    }

    internal T Value { get; private set; }
    internal bool IsMarked { get; set; }

    internal int From { get; private set; }
    internal int To { get; private set; }
  }
}