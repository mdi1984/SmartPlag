namespace SmartPlag.Comparison.Algorithm.GreedyStringTiling.Model
{
  public class Match
  {
    internal Match(int patternIndex, int textIndex, int length)
    {
      this.PatternIndex = patternIndex;
      this.TextIndex = textIndex;
      this.Length = length;
    }

    public int PatternIndex { get; private set; }
    public int TextIndex { get; private set; }
    public int Length { get; private set; }
  }
}