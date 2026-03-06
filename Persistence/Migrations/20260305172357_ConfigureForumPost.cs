using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class ConfigureForumPost : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ForumPost_ForumRooms_ForumRoomId",
                table: "ForumPost");

            migrationBuilder.DropForeignKey(
                name: "FK_ForumPost_Users_UserId",
                table: "ForumPost");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ForumPost",
                table: "ForumPost");

            migrationBuilder.RenameTable(
                name: "ForumPost",
                newName: "ForumPosts");

            migrationBuilder.RenameIndex(
                name: "IX_ForumPost_UserId",
                table: "ForumPosts",
                newName: "IX_ForumPosts_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_ForumPost_ForumRoomId",
                table: "ForumPosts",
                newName: "IX_ForumPosts_ForumRoomId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ForumPosts",
                table: "ForumPosts",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ForumPosts_ForumRooms_ForumRoomId",
                table: "ForumPosts",
                column: "ForumRoomId",
                principalTable: "ForumRooms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ForumPosts_Users_UserId",
                table: "ForumPosts",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ForumPosts_ForumRooms_ForumRoomId",
                table: "ForumPosts");

            migrationBuilder.DropForeignKey(
                name: "FK_ForumPosts_Users_UserId",
                table: "ForumPosts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ForumPosts",
                table: "ForumPosts");

            migrationBuilder.RenameTable(
                name: "ForumPosts",
                newName: "ForumPost");

            migrationBuilder.RenameIndex(
                name: "IX_ForumPosts_UserId",
                table: "ForumPost",
                newName: "IX_ForumPost_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_ForumPosts_ForumRoomId",
                table: "ForumPost",
                newName: "IX_ForumPost_ForumRoomId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ForumPost",
                table: "ForumPost",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ForumPost_ForumRooms_ForumRoomId",
                table: "ForumPost",
                column: "ForumRoomId",
                principalTable: "ForumRooms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ForumPost_Users_UserId",
                table: "ForumPost",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}
