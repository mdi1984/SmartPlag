using System.Collections.Generic;
using SmartPlag.Manager.SimpleManager.Infrastructure.Model;

namespace SmartPlag.Manager.SimpleManager.Model
{
  public class AssignmentModel
  {
    public Assignment Assignment { get; set; }
    public List<TokenizerService> TokenizerServices { get; set; }
    public List<ComparisonService> ComparisonServices { get; set; }
  }
}