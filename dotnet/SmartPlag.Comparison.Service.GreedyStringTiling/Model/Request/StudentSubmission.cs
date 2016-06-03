using System.Collections.Generic;

namespace SmartPlag.Comparison.Service.GreedyStringTiling.Model.Request
{
  public class StudentSubmission
  {
    public string Name { get; set; }
    public List<TokennizedFile> Files { get; set; }
  }
}