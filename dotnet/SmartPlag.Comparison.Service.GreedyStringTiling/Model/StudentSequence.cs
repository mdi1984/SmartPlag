using System.Collections.Generic;
using SmartPlag.Comparison.Algorithm.GreedyStringTiling.Model;

namespace SmartPlag.Comparison.Service.GreedyStringTiling.Model
{
  public class StudentSequence
  {
    public string Name { get; set; }
    public Sequence<int> TokenSequence { get; set; }
  }
}