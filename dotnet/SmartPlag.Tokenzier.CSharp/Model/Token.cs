namespace SmartPlag.Tokenzier.CSharp.Model
{
  public class Token
  {
    public Token(string name, int code)
    {
      this.Name = name;
      this.Code = code;
    }

    public string Name { get; set; }
    public int Code { get; set; }
  }
}
