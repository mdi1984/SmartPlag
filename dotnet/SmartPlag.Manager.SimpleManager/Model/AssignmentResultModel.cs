﻿using System.Collections.Generic;
using SmartPlag.Manager.Simple.EF.Model;

namespace SmartPlag.Manager.SimpleManager.Model
{
  public class AssignmentResultModel
  {
    public Assignment Assignment { get; set; }
    public string FirstName { get; set; }
    public string SecondName { get; set; }
    public string FirstSource { get; set; }
    public string SecondSource { get; set; }
    public List<MatchPosition> MatchPositions { get; set; }
  }

  public class MatchPosition
  {
    public int PatternStart { get; set; }
    public int TextStart { get; set; }
    public int PatternLength { get; set; }
    public int TextLength { get; set; }
    public string RandomHexColor { get; set; }
  }
}