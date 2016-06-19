using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartPlag.Manager.Simple.EF.Model
{
  public class Match : PlagEntity
  {
    public int ResultId { get; set; }
    public Result Result { get; set; }
    public int PatternIndex { get; set; }
    public int TextIndex { get; set; }
    public int TokenLength { get; set; }
  }
}
