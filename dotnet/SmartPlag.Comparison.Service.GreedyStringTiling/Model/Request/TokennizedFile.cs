﻿using System.Collections.Generic;

namespace SmartPlag.Comparison.Service.GreedyStringTiling.Model.Request
{
  public class TokennizedFile
  {
    public string FileName { get; set; }
    public List<int> Tokens { get; set; }
  }
}