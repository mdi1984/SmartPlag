using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartPlag.Manager.Simple.EF.Model
{
  public class Submission : PlagEntity
  {
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public int AssignmentId { get; set; }
    public Assignment Assignment { get; set; }
    public ICollection<StudentFile> Files { get; set; }
  }
}
