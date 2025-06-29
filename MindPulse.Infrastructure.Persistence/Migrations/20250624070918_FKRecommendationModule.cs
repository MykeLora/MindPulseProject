using Microsoft.EntityFrameworkCore.Migrations;
using System;
#nullable disable

namespace MindPulse.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class FKRecommendationModule : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Recommendations_Users_UserId",
                table: "Recommendations");

            migrationBuilder.AddForeignKey(
                name: "FK_Recommendations_Users_UserId",
                table: "Recommendations",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Recommendations_Users_UserId",
                table: "Recommendations");

            migrationBuilder.AddForeignKey(
                name: "FK_Recommendations_Users_UserId",
                table: "Recommendations",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
