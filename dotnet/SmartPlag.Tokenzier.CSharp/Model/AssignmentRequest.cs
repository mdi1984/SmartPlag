using System.Collections.Generic;

namespace SmartPlag.Tokenzier.CSharp.Model
{
  public class AssignmentRequest
  {
    public string Title { get; set; }
    public List<StudentAssignment> Assignments { get; set; }
  }
}
