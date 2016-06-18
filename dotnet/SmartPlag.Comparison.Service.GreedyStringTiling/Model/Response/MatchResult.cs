using System.Collections.Generic;
using SmartPlag.Comparison.Algorithm.GreedyStringTiling.Model;

namespace SmartPlag.Comparison.Service.GreedyStringTiling.Model.Response
{
  public class MatchResult
  {
    public MatchResult(Student first, Student second)
    {
      this.First = first;
      this.Second = second;
    }

    public Student First { get; set; }
    public Student Second { get; set; }
    public IList<Match> Matches { get; set; } = new List<Match>();
  }
}