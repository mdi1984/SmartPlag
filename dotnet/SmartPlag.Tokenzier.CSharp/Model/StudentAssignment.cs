using System.Collections.Generic;

namespace SmartPlag.Tokenzier.CSharp.Model
{
  public class StudentAssignment
  {
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public List<StudentFile> Files { get; set; }
  }
}