using System.Collections.Generic;

namespace SmartPlag.CSharp.Model
{
  public class AssignmentResponse
  {
    public AssignmentResponse()
    {
      this.StudentResults = new List<StudentTokenMap>();
    }

    public AssignmentResponse(string title)
      : this()
    {
      this.Title = title;
    }

    public string Title { get; set; }
    public List<StudentTokenMap> StudentResults { get; set; }
  }
}