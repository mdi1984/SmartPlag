using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartPlag.Manager.SimpleManager.Infrastructure.Model
{
  public class ComparatorResult
  {
    public Student First { get; set; }
    public Student Second { get; set; }
    public List<Match> Matches { get; set; }
  }

  public class Student
  {
    public string FirstName { get; set; }
    public string LastName { get; set; }
  }

  public class Match
  {
    public int PatternIndex { get; set; }
    public int TextIndex { get; set; }
    public int Length { get; set; }
  }
}
