using System.Collections.Generic;

namespace SmartPlag.Manager.SimpleManager.Infrastructure.Model
{
  public class Assignment
  {
    public int Id { get; set; }
    public string Title { get; set; }
    public int TokenizerServiceId { get; set; }
    public TokenizerService TokenizerService { get; set; }
    public int ComparisonServiceId { get; set; }

    public ComparisonService ComparisonService { get; set; }
    public ICollection<Submission> Submissions { get; set; }
  }
}