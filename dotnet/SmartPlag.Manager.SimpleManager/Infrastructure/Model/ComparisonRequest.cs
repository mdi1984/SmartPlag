using System.Collections.Generic;

namespace SmartPlag.Manager.SimpleManager.Infrastructure.Model
{
  public class ComparisonRequest
  {
    public List<Submission> Submissions { get; internal set; }
    public string Title { get; internal set; }
  }

  public class Submission
  {
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public List<TokenizedFile> Files { get; set; }
  }

  public class TokenizedFile
  {
    public string FileName { get; set; }
    public List<int> Tokens { get; set; }
  }
}