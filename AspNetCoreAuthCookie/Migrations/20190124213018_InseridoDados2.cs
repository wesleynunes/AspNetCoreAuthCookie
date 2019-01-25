using Microsoft.EntityFrameworkCore.Migrations;

namespace AspNetCoreAuthCookie.Migrations
{
    public partial class InseridoDados2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 1,
                column: "UserTypes",
                value: 1);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 1,
                column: "UserTypes",
                value: 0);
        }
    }
}
