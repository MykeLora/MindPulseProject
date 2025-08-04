using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MindPulse.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedTestResults : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CompletionDate",
                table: "TestResults");

            migrationBuilder.RenameColumn(
                name: "SeverityScore",
                table: "TestResults",
                newName: "Confidence");

            migrationBuilder.AlterColumn<string>(
                name: "Summary",
                table: "TestResults",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500,
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Confidence",
                table: "TestResults",
                newName: "SeverityScore");

            migrationBuilder.AlterColumn<string>(
                name: "Summary",
                table: "TestResults",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(1000)",
                oldMaxLength: 1000,
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CompletionDate",
                table: "TestResults",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
