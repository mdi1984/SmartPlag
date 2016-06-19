using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using SmartPlag.Manager.Simple.EF;

namespace SmartPlag.Manager.Simple.EF.Migrations
{
    [DbContext(typeof(PlagContext))]
    partial class PlagContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.0.0-rc2-20901")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("SmartPlag.Manager.Simple.EF.Model.Assignment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("ComparisonServiceId");

                    b.Property<DateTime>("Created");

                    b.Property<string>("Owner");

                    b.Property<int>("State");

                    b.Property<string>("Title");

                    b.Property<int>("TokenizerServiceId");

                    b.HasKey("Id");

                    b.HasIndex("ComparisonServiceId");

                    b.HasIndex("TokenizerServiceId");

                    b.ToTable("Assignments");
                });

            modelBuilder.Entity("SmartPlag.Manager.Simple.EF.Model.ComparisonService", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("BaseUrl");

                    b.Property<string>("Description");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("ComparisonServices");
                });

            modelBuilder.Entity("SmartPlag.Manager.Simple.EF.Model.Match", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("PatternIndex");

                    b.Property<int>("ResultId");

                    b.Property<int>("TextIndex");

                    b.Property<int>("TokenLength");

                    b.HasKey("Id");

                    b.HasIndex("ResultId");

                    b.ToTable("Match");
                });

            modelBuilder.Entity("SmartPlag.Manager.Simple.EF.Model.Result", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AssignmentId");

                    b.Property<int>("FirstId");

                    b.Property<int>("SecondId");

                    b.HasKey("Id");

                    b.HasIndex("AssignmentId");

                    b.HasIndex("FirstId");

                    b.HasIndex("SecondId");

                    b.ToTable("Results");
                });

            modelBuilder.Entity("SmartPlag.Manager.Simple.EF.Model.StudentFile", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Content");

                    b.Property<string>("FileName");

                    b.Property<int>("SubmissionId");

                    b.Property<string>("TokenizedContent");

                    b.HasKey("Id");

                    b.HasIndex("SubmissionId");

                    b.ToTable("Files");
                });

            modelBuilder.Entity("SmartPlag.Manager.Simple.EF.Model.Submission", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AssignmentId");

                    b.Property<string>("FirstName");

                    b.Property<string>("LastName");

                    b.HasKey("Id");

                    b.HasIndex("AssignmentId");

                    b.ToTable("Submissions");
                });

            modelBuilder.Entity("SmartPlag.Manager.Simple.EF.Model.TokenizerService", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("BaseUrl");

                    b.Property<string>("Description");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("TokenizerServices");
                });

            modelBuilder.Entity("SmartPlag.Manager.Simple.EF.Model.Assignment", b =>
                {
                    b.HasOne("SmartPlag.Manager.Simple.EF.Model.ComparisonService")
                        .WithMany()
                        .HasForeignKey("ComparisonServiceId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("SmartPlag.Manager.Simple.EF.Model.TokenizerService")
                        .WithMany()
                        .HasForeignKey("TokenizerServiceId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("SmartPlag.Manager.Simple.EF.Model.Match", b =>
                {
                    b.HasOne("SmartPlag.Manager.Simple.EF.Model.Result")
                        .WithMany()
                        .HasForeignKey("ResultId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("SmartPlag.Manager.Simple.EF.Model.Result", b =>
                {
                    b.HasOne("SmartPlag.Manager.Simple.EF.Model.Assignment")
                        .WithMany()
                        .HasForeignKey("AssignmentId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("SmartPlag.Manager.Simple.EF.Model.Submission")
                        .WithMany()
                        .HasForeignKey("FirstId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("SmartPlag.Manager.Simple.EF.Model.Submission")
                        .WithMany()
                        .HasForeignKey("SecondId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("SmartPlag.Manager.Simple.EF.Model.StudentFile", b =>
                {
                    b.HasOne("SmartPlag.Manager.Simple.EF.Model.Submission")
                        .WithMany()
                        .HasForeignKey("SubmissionId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("SmartPlag.Manager.Simple.EF.Model.Submission", b =>
                {
                    b.HasOne("SmartPlag.Manager.Simple.EF.Model.Assignment")
                        .WithMany()
                        .HasForeignKey("AssignmentId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
