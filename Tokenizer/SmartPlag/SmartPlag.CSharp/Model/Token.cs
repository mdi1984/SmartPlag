using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartPlag.CSharp.Model
{
  public class Token
  {
    public Token(string name, int code)
    {
      this.Name = name;
      this.Code = code;
    }

    public string Name { get; set; }
    public int Code { get; set; }
  }
}
