using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace SmartPlag.Manager.Simple.EF
{
  public class PlagContext : DbContext
  {
    public PlagContext(DbContextOptions<PlagContext> options)
      : base(options)
    { }
    
  }
}
