namespace SmartPlag.Comparison.GreedyStringTiling.Model
{
  public class Match
  {
    internal Match(int patternPosition, int textPosition, int length)
    {
      this.PatternPosition = patternPosition;
      this.TextPosition = textPosition;
      this.Length = length;
    }

    public int PatternPosition { get; private set; }
    public int TextPosition { get; private set; }
    public int Length { get; private set; }
  }
}