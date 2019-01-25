using Microsoft.EntityFrameworkCore.Migrations;

namespace AspNetCoreAuthCookie.Migrations
{
    public partial class AlterandoDadosInseridosUser3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "ActiveUser", "Password", "RememberMe", "UserName", "UserTypes" },
                values: new object[] { 2, true, "EF797C8118F02DFB64967DD5D3F8C762348C9C63D532CC95C5ED7A898A64F", true, "Manager", 1 });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "ActiveUser", "Password", "RememberMe", "UserName", "UserTypes" },
                values: new object[] { 3, true, "EF797C8118F02DFB64967DD5D3F8C762348C9C63D532CC95C5ED7A898A64F", true, "Users", 2 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 3);
        }
    }
}
