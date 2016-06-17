using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SmartPlag.Manager.Simple.EF.Migrations
{
    public partial class AddServicesToAssignment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ComparisonServiceId",
                table: "Assignments",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TokenizerServiceId",
                table: "Assignments",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Assignments_ComparisonServiceId",
                table: "Assignments",
                column: "ComparisonServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_Assignments_TokenizerServiceId",
                table: "Assignments",
                column: "TokenizerServiceId");

            migrationBuilder.AddForeignKey(
                name: "FK_Assignments_ComparisonServices_ComparisonServiceId",
                table: "Assignments",
                column: "ComparisonServiceId",
                principalTable: "ComparisonServices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Assignments_TokenizerServices_TokenizerServiceId",
                table: "Assignments",
                column: "TokenizerServiceId",
                principalTable: "TokenizerServices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Assignments_ComparisonServices_ComparisonServiceId",
                table: "Assignments");

            migrationBuilder.DropForeignKey(
                name: "FK_Assignments_TokenizerServices_TokenizerServiceId",
                table: "Assignments");

            migrationBuilder.DropIndex(
                name: "IX_Assignments_ComparisonServiceId",
                table: "Assignments");

            migrationBuilder.DropIndex(
                name: "IX_Assignments_TokenizerServiceId",
                table: "Assignments");

            migrationBuilder.DropColumn(
                name: "ComparisonServiceId",
                table: "Assignments");

            migrationBuilder.DropColumn(
                name: "TokenizerServiceId",
                table: "Assignments");
        }
    }
}
