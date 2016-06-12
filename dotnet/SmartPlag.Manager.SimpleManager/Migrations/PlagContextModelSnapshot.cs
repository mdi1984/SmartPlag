using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using SmartPlag.Manager.SimpleManager.Infrastructure;

namespace SmartPlag.Manager.SimpleManager.Migrations
{
    [DbContext(typeof(PlagContext))]
    partial class PlagContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.0.0-rc2-20901")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("SmartPlag.Manager.SimpleManager.Infrastructure.Model.Assignment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("ComparisonServiceId");

                    b.Property<string>("Title");

                    b.Property<int>("TokenizerServiceId");

                    b.HasKey("Id");

                    b.HasIndex("ComparisonServiceId");

                    b.HasIndex("TokenizerServiceId");

                    b.ToTable("Assignments");
                });

            modelBuilder.Entity("SmartPlag.Manager.SimpleManager.Infrastructure.Model.ComparisonService", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.Property<string>("ServiceUrl");

                    b.HasKey("Id");

                    b.ToTable("ComparisonServices");
                });

            modelBuilder.Entity("SmartPlag.Manager.SimpleManager.Infrastructure.Model.Submission", b =>
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

            modelBuilder.Entity("SmartPlag.Manager.SimpleManager.Infrastructure.Model.SubmissionFile", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Content");

                    b.Property<string>("FileName");

                    b.Property<int>("SubmissionId");

                    b.Property<string>("TokenizedJsonContent");

                    b.HasKey("Id");

                    b.HasIndex("SubmissionId");

                    b.ToTable("SubmissionFiles");
                });

            modelBuilder.Entity("SmartPlag.Manager.SimpleManager.Infrastructure.Model.TokenizerService", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.Property<string>("ServiceUrl");

                    b.HasKey("Id");

                    b.ToTable("TokenizerServices");
                });

            modelBuilder.Entity("SmartPlag.Manager.SimpleManager.Infrastructure.Model.Assignment", b =>
                {
                    b.HasOne("SmartPlag.Manager.SimpleManager.Infrastructure.Model.ComparisonService")
                        .WithMany()
                        .HasForeignKey("ComparisonServiceId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("SmartPlag.Manager.SimpleManager.Infrastructure.Model.TokenizerService")
                        .WithMany()
                        .HasForeignKey("TokenizerServiceId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("SmartPlag.Manager.SimpleManager.Infrastructure.Model.Submission", b =>
                {
                    b.HasOne("SmartPlag.Manager.SimpleManager.Infrastructure.Model.Assignment")
                        .WithMany()
                        .HasForeignKey("AssignmentId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("SmartPlag.Manager.SimpleManager.Infrastructure.Model.SubmissionFile", b =>
                {
                    b.HasOne("SmartPlag.Manager.SimpleManager.Infrastructure.Model.Submission")
                        .WithMany()
                        .HasForeignKey("SubmissionId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
