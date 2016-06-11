using System.Collections.Generic;

namespace SmartPlag.Manager.SimpleManager.Infrastructure.Model
{
  public class Submission
  {
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public ICollection<SubmissionFile> Files { get; set; }
  }
}