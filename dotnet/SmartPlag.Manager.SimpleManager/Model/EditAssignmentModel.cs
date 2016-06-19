using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SmartPlag.Manager.Simple.EF.Model;

namespace SmartPlag.Manager.SimpleManager.Model
{
  public class EditAssignmentModel
  {
    public int? Id { get; set; }
    public string Title { get; set; }

    public int? TokenizerServiceId { get; set; }
    public TokenizerService TokenizerService { get; set; }

    public int? ComparisonServiceId { get; set; }
    public ComparisonService ComparisonService { get; set; }
  }
}
