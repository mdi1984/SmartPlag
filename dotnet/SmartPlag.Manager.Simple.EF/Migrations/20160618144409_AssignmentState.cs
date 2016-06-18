using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SmartPlag.Manager.Simple.EF.Migrations
{
    public partial class AssignmentState : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Evaluating",
                table: "Assignments");

            migrationBuilder.AddColumn<int>(
                name: "State",
                table: "Assignments",
                nullable: false,
                defaultValue: Model.AssignmentState.Open);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "State",
                table: "Assignments");

            migrationBuilder.AddColumn<bool>(
                name: "Evaluating",
                table: "Assignments",
                nullable: false,
                defaultValue: false);
        }
    }
}
