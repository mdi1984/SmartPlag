using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartPlag.Manager.Simple.EF.Model
{
  public class Result : PlagEntity
  {
    public int AssignmentId { get; set; }
    public Assignment Assignment { get; set; }
    public int FirstId { get; set; }
    public Submission First { get; set; }
    public int SecondId { get; set; }
    public Submission Second { get; set; }
    public List<Match> Matches { get; set; }
  }
}
