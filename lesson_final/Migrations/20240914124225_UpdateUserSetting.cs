using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace lesson_final.Migrations
{
    /// <inheritdoc />
    public partial class UpdateUserSetting : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserSetting_Users_UserId",
                table: "UserSetting");

            migrationBuilder.DropIndex(
                name: "IX_UserSetting_UserId",
                table: "UserSetting");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "UserSetting");

            migrationBuilder.CreateIndex(
                name: "IX_Users_UserSettingId",
                table: "Users",
                column: "UserSettingId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_UserSetting_UserSettingId",
                table: "Users",
                column: "UserSettingId",
                principalTable: "UserSetting",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_UserSetting_UserSettingId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_UserSettingId",
                table: "Users");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "UserSetting",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_UserSetting_UserId",
                table: "UserSetting",
                column: "UserId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_UserSetting_Users_UserId",
                table: "UserSetting",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
