using Microsoft.EntityFrameworkCore.Migrations;

namespace AspNetCoreAuthCookie.Migrations
{
    public partial class AlterandoDadosInseridosUser2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 1,
                column: "Password",
                value: "EF797C8118F02DFB64967DD5D3F8C762348C9C63D532CC95C5ED7A898A64F");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 1,
                column: "Password",
                value: "8BB0CF6EB9B17DF7D22B456F121257DC1254E1F1665370476383EA776DF414");
        }
    }
}
