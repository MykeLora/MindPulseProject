using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MindPulse.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class ConfiguredRecommendAndEmotions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmotionalRecords");

            migrationBuilder.DropColumn(
                name: "UrlSource",
                table: "Recommendations");

            migrationBuilder.DropColumn(
                name: "Emotion",
                table: "EmotionalHistories");

            migrationBuilder.RenameColumn(
                name: "Score",
                table: "EmotionalHistories",
                newName: "Confidence");

            migrationBuilder.AddColumn<int>(
                name: "EducationalContentId",
                table: "Recommendations",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Summary",
                table: "EmotionalHistories",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Recommendations_EducationalContentId",
                table: "Recommendations",
                column: "EducationalContentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Recommendations_EducationalContents_EducationalContentId",
                table: "Recommendations",
                column: "EducationalContentId",
                principalTable: "EducationalContents",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Recommendations_EducationalContents_EducationalContentId",
                table: "Recommendations");

            migrationBuilder.DropIndex(
                name: "IX_Recommendations_EducationalContentId",
                table: "Recommendations");

            migrationBuilder.DropColumn(
                name: "EducationalContentId",
                table: "Recommendations");

            migrationBuilder.DropColumn(
                name: "Summary",
                table: "EmotionalHistories");

            migrationBuilder.RenameColumn(
                name: "Confidence",
                table: "EmotionalHistories",
                newName: "Score");

            migrationBuilder.AddColumn<string>(
                name: "UrlSource",
                table: "Recommendations",
                type: "nvarchar(350)",
                maxLength: 350,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Emotion",
                table: "EmotionalHistories",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "EmotionalRecords",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Confidence = table.Column<double>(type: "float", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DetectedEmotion = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Text = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmotionalRecords", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmotionalRecords_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EmotionalRecords_UserId",
                table: "EmotionalRecords",
                column: "UserId");
        }
    }
}
