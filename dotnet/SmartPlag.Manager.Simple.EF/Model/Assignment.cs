using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartPlag.Manager.Simple.EF.Model
{
  public class Assignment : PlagEntity
  {
    public string Title { get; set; }
    public ICollection<Submission> Submissions { get; set; }
    public string Owner { get; set; }
    public DateTime Created { get; set; } = DateTime.UtcNow;
  }
}
