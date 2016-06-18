using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SmartPlag.Manager.Simple.EF.Model;

namespace SmartPlag.Manager.Simple.EF
{
  public class PlagContext : DbContext
  {
    // uncomment to enable migrations
    // public PlagContext()
    // { }

    // protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    // {
    //   optionsBuilder.UseSqlServer(
    //     "Data Source=MDI-XMG\\SQLEXPRESS;Initial Catalog=SmartPlagSimpleManagerV2;Integrated Security=True;Connect Timeout=15;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
    // }

    public PlagContext(DbContextOptions<PlagContext> options)
      : base(options)
    { }

    public DbSet<Assignment> Assignments { get; set; }
    public DbSet<Submission> Submissions { get; set; }
    public DbSet<StudentFile> Files { get; set; }
    public DbSet<TokenizerService> TokenizerServices { get; set; }
    public DbSet<ComparisonService> ComparisonServices { get; set; }
    public DbSet<Result> Results { get; set; }
  }
}
