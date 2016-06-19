using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SmartPlag.Manager.Simple.EF.Model;

namespace SmartPlag.Manager.SimpleManager.Model
{
  public class SubmissionListModel
  {
    public Assignment Assignment { get; set; }
    public IEnumerable<Submission> Submissions { get; set; }
  }
}
