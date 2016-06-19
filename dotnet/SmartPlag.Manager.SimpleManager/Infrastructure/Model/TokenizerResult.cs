using System.Collections.Generic;

namespace SmartPlag.Manager.SimpleManager.Infrastructure.Model
{
  public class TokenizerResult
  {
    public string Title { get; set; }
    public List<StudentResult> StudentResults { get; set; }
  }

  public class StudentResult
  {
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public List<FileResult> Files { get; set; }
  }

  public class FileResult
  {
    public string FileName { get; set; }
    public object Tokens { get; set; }
    //public List<TokenResult> Tokens { get; set; }
  }

  public class TokenResult
  {
    public int Token { get; set; }
    public int From { get; set; }
    public int To { get; set; }
  }
}