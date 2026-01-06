using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineCourse.Data.Migrations
{
    /// <inheritdoc />
    public partial class migration_file_0001 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "RefreshToken",
                table: "users",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "RefreshTokenExpireTime",
                table: "users",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RefreshToken",
                table: "users");

            migrationBuilder.DropColumn(
                name: "RefreshTokenExpireTime",
                table: "users");
        }
    }
}
