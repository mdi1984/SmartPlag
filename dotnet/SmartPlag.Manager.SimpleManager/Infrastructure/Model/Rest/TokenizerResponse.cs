using System.Collections.Generic;

namespace SmartPlag.Manager.SimpleManager.Infrastructure.Model.Rest
{
  public class TokenizerResponse
  {
    public string Title { get; set; }
    public List<StudentResult> StudentResults { get; set; }
  }

  public class StudentResult
  {
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public List<ResultFile> Files { get; set; }
  }

  public class ResultFile
  {
    public string FileName { get; set; }
    public string Tokens { get; set; }
  }
}