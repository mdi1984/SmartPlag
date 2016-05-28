namespace SmartPlag.CSharp.Model
{
  public class TokenWithPosition
  {
    public TokenWithPosition(int token, int from, int to)
    {
      this.Token = token;
      this.From = from;
      this.To = to;
    }

    public int Token { get; set; }
    public int From { get; set; }
    public int To { get; set; }
  }
}