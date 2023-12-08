using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TeachToEach.DAL.Migrations
{
    public partial class AddUserRegisterFields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "login",
                table: "Users",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "password",
                table: "Users",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "role",
                table: "Users",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "login",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "password",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "role",
                table: "Users");
        }
    }
}
