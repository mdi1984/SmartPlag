using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SmartPlag.Manager.Simple.EF.Migrations
{
    public partial class ServicesSpecifyRequestPath : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "RequestPath",
                table: "TokenizerServices",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RequestPath",
                table: "ComparisonServices",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RequestPath",
                table: "TokenizerServices");

            migrationBuilder.DropColumn(
                name: "RequestPath",
                table: "ComparisonServices");
        }
    }
}
