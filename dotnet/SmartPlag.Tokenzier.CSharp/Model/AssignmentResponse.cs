using System.Collections.Generic;

namespace SmartPlag.Tokenzier.CSharp.Model
{
  public class AssignmentResponse
  {
    public AssignmentResponse()
    {
      this.StudentResults = new List<StudentResult>();
    }

    public AssignmentResponse(string title)
      : this()
    {
      this.Title = title;
    }

    public string Title { get; set; }
    public List<StudentResult> StudentResults { get; set; }
  }
}