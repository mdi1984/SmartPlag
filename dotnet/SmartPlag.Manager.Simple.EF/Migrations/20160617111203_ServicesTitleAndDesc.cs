using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SmartPlag.Manager.Simple.EF.Migrations
{
    public partial class ServicesTitleAndDesc : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "TokenizerServices",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "TokenizerServices",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "ComparisonServices",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "ComparisonServices",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "TokenizerServices");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "TokenizerServices");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "ComparisonServices");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "ComparisonServices");
        }
    }
}
