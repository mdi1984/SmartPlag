using System.Collections.Generic;

namespace SmartPlag.Manager.SimpleManager.Infrastructure.Model
{
  public class Assignment
  {
    public int Id { get; set; }
    public string Title { get; set; }
    public ICollection<Submission> Submissions { get; set; }
  }
}