using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BasicAuthenticationAPI.Data.Migrations
{
    /// <inheritdoc />
    public partial class admin : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "EndUsers",
                columns: new[] { "EndUserId", "FirstName", "LastName", "Password", "Username" },
                values: new object[] { 1, "Jerry", "Yrrej", "$2a$11$822agt0y59tvOuQZK9URVOB37XSUXwyJ.7lzOpMJuECEb.OJKP3Wa", "admin" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "EndUsers",
                keyColumn: "EndUserId",
                keyValue: 1);
        }
    }
}
