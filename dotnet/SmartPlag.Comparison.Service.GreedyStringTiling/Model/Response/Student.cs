using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SmartPlag.Comparison.Algorithm.GreedyStringTiling.Model;

namespace SmartPlag.Comparison.Service.GreedyStringTiling.Model.Response
{
  public class Student
  {
    public Student(string firstName, string lastName)
    {
      FirstName = firstName;
      LastName = lastName;
    }

    public string FirstName { get; set; }
    public string LastName { get; set; }
  }
}
