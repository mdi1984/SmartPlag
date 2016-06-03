using System.Collections.Generic;

namespace SmartPlag.Comparison.Service.GreedyStringTiling.Model.Request
{
  public class ComparisonRequest
  {
    public string Title { get; set; }
    public List<StudentSubmission> Submissions { get; set; }
  }
}