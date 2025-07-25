using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MindPulse.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddSummaryToEmotionAnalysis : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Summary",
                table: "EmotionAnalysis",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Summary",
                table: "EmotionAnalysis");
        }
    }
}
