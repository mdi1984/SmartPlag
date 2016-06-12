using System;
using Microsoft.EntityFrameworkCore;
using SmartPlag.Manager.SimpleManager.Infrastructure.Model;

namespace SmartPlag.Manager.SimpleManager.Infrastructure
{
  public class PlagContext : DbContext
  {
    public PlagContext(DbContextOptions<PlagContext> options)
      : base(options) { }

    public DbSet<Assignment> Assignments { get; set; }
    public DbSet<Submission> Submissions { get; set; }
    public DbSet<SubmissionFile> SubmissionFiles { get; set; }
    public DbSet<TokenizerService> TokenizerServices { get; set; }
    public DbSet<ComparisonService> ComparisonServices { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      base.OnModelCreating(modelBuilder);
    }
  }
}