using System.Collections.Generic;

namespace SmartPlag.Comparison.Service.GreedyStringTiling.Model
{
  public class ComparisonRequest
  {
    public string Title { get; set; }
    public List<StudentSubmission> Submissions { get; set; }
  }
}