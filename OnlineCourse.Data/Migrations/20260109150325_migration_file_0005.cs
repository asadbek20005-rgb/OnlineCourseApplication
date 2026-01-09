using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineCourse.Data.Migrations
{
    /// <inheritdoc />
    public partial class migration_file_0005 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_contents_lessons_lesson_id",
                table: "contents");

            migrationBuilder.DropIndex(
                name: "IX_contents_lesson_id",
                table: "contents");

            migrationBuilder.DropColumn(
                name: "lesson_id",
                table: "contents");

            migrationBuilder.AddColumn<int>(
                name: "video_content_id",
                table: "lessons",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_lessons_video_content_id",
                table: "lessons",
                column: "video_content_id");

            migrationBuilder.AddForeignKey(
                name: "FK_lessons_contents_video_content_id",
                table: "lessons",
                column: "video_content_id",
                principalTable: "contents",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_lessons_contents_video_content_id",
                table: "lessons");

            migrationBuilder.DropIndex(
                name: "IX_lessons_video_content_id",
                table: "lessons");

            migrationBuilder.DropColumn(
                name: "video_content_id",
                table: "lessons");

            migrationBuilder.AddColumn<int>(
                name: "lesson_id",
                table: "contents",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_contents_lesson_id",
                table: "contents",
                column: "lesson_id");

            migrationBuilder.AddForeignKey(
                name: "FK_contents_lessons_lesson_id",
                table: "contents",
                column: "lesson_id",
                principalTable: "lessons",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
