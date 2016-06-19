using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using SmartPlag.Manager.Simple.EF;

namespace SmartPlag.Manager.Simple.EF.Migrations
{
    [DbContext(typeof(PlagContext))]
    [Migration("20160617095302_InitEntities")]
    partial class InitEntities
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.0.0-rc2-20901")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("SmartPlag.Manager.Simple.EF.Model.Assignment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Created");

                    b.Property<string>("Owner");

                    b.Property<string>("Title");

                    b.HasKey("Id");

                    b.ToTable("Assignments");
                });

            modelBuilder.Entity("SmartPlag.Manager.Simple.EF.Model.StudentFile", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Content");

                    b.Property<int>("SubmissionId");

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
