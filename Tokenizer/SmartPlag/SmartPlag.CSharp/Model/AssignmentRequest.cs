using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartPlag.CSharp.Model
{
  public class AssignmentRequest
  {
    public string Title { get; set; }
    public List<StudentAssignment> Assignments { get; set; }
  }
}
