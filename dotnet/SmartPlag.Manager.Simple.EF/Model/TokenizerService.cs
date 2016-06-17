using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartPlag.Manager.Simple.EF.Model
{
  public class TokenizerService : PlagEntity
  {
    public string Name { get; set; }
    public string Description { get; set; }
    public string BaseUrl { get; set; }
  }
}
