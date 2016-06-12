using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartPlag.Manager.SimpleManager.Infrastructure.Model.Rest
{
  public class TokenizerRequest
  {
    public string Title { get; set; }
    public List<Rest.Assignment> Assignments { get; set; }
  }

  public class Assignment
  {
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public List<File> Files { get; set; }
  }

  public class File
  {
    public string FileName { get; set; }
    public string Base64Source { get; set; }
  }
}
