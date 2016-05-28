using System.Collections.Generic;

namespace SmartPlag.CSharp.Model
{
  public class StudentTokenMap
  {
    public StudentTokenMap()
    {
      this.Tokens = new List<TokenWithPosition>();
    }

    public StudentTokenMap(string firstName, string lastName)
      : this()
    {
      this.FirstName = firstName;
      this.LastName = lastName;
    }

    public string FirstName { get; set; }
    public string LastName { get; set; }
    public List<TokenWithPosition> Tokens { get; set; }
  }
}