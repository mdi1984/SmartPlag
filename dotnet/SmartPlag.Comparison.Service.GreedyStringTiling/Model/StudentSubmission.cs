using System.Collections.Generic;

namespace SmartPlag.Comparison.Service.GreedyStringTiling.Model
{
  public class StudentSubmission
  {
    public string Name { get; set; }
    public List<TokennizedFile> Files { get; set; }
  }
}