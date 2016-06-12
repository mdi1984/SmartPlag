using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartPlag.Manager.SimpleManager.Infrastructure.Model
{
  public class SubmissionFile
  {
    public int Id { get; set; }
    public int SubmissionId { get; set; }
    public Submission Submission { get; set; }
    public string FileName { get; set; }
    public string Content { get; set; }
    public string TokenizedJsonContent { get; set; }
  }
}
