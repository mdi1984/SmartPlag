using System.Collections.Generic;

namespace SmartPlag.Tokenzier.CSharp.Model
{
  public class StudentTokenMap
  {
    public StudentTokenMap()
    {
      this.Tokens = new List<TokenWithPosition>();
    }

    public StudentTokenMap(string fileName)
      : this()
    {
      this.FileName = fileName;
    }

    public string FileName { get; set; }

    public List<TokenWithPosition> Tokens { get; set; }
  }
}