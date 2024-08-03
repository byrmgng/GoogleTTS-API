using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TextToSpeechAPI.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class mig2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "RefleshToken",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "RefleshTokenEndDate",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RefleshToken",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "RefleshTokenEndDate",
                table: "AspNetUsers");
        }
    }
}
