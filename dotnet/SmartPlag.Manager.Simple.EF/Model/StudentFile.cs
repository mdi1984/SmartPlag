using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartPlag.Manager.Simple.EF.Model
{
  public class StudentFile : PlagEntity
  {
    public string Content { get; set; }
    public int SubmissionId { get; set; }
    public Submission Submission { get; set; }
  }
}
