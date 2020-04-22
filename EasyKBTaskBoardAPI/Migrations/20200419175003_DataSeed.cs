using Microsoft.EntityFrameworkCore.Migrations;

namespace EasyKBTaskBoard.API.Migrations
{
    public partial class DataSeed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Accounts",
                columns: new[] { "Id", "Email", "FirstName", "LastName", "Password", "TaskId" },
                values: new object[] { 1, "akolenda73@gmail.com", "Andreas", "Kolenda", "test123", null });

            migrationBuilder.InsertData(
                table: "Accounts",
                columns: new[] { "Id", "Email", "FirstName", "LastName", "Password", "TaskId" },
                values: new object[] { 2, "carlo.tamburin@gmail.com", "Carlo", "Tamburin", "test321", null });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
