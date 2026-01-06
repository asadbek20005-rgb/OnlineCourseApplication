using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineCourse.Data.Migrations
{
    /// <inheritdoc />
    public partial class migration_file_0002 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_users_contents_photo_content_id",
                table: "users");

            migrationBuilder.AlterColumn<int>(
                name: "photo_content_id",
                table: "users",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddForeignKey(
                name: "FK_users_contents_photo_content_id",
                table: "users",
                column: "photo_content_id",
                principalTable: "contents",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_users_contents_photo_content_id",
                table: "users");

            migrationBuilder.AlterColumn<int>(
                name: "photo_content_id",
                table: "users",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_users_contents_photo_content_id",
                table: "users",
                column: "photo_content_id",
                principalTable: "contents",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
