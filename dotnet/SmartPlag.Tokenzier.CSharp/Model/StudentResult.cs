using System.Collections.Generic;

namespace SmartPlag.Tokenzier.CSharp.Model
{
  public class StudentResult
  {
    public StudentResult()
    {
      this.Files = new List<StudentTokenMap>();
    }

    public StudentResult(string firstName, string lastName)
      : this()
    {
      this.FirstName = firstName;
      this.LastName = lastName;
    }

    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public List<StudentTokenMap> Files { get; set; }

  }
}