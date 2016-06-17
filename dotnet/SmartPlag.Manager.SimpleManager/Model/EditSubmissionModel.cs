using System.Collections.Generic;
using SmartPlag.Manager.Simple.EF.Model;

namespace SmartPlag.Manager.SimpleManager.Model
{
  public class EditSubmissionModel
  {
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public List<StudentFile> Files { get; set; }
  }
}