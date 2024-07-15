using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BankSystem.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Auth0IdAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Auth0Id",
                schema: "bankdb",
                table: "Clients",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Auth0Id",
                schema: "bankdb",
                table: "Clients");
        }
    }
}
