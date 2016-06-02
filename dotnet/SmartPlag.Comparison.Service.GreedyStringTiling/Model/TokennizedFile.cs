using System.Collections.Generic;

namespace SmartPlag.Comparison.Service.GreedyStringTiling.Model
{
  public class TokennizedFile
  {
    public string FileName { get; set; }
    public List<int> Tokens { get; set; }
  }
}