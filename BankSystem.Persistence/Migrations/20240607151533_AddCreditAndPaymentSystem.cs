using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BankSystem.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddCreditAndPaymentSystem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Email",
                schema: "bankdb",
                table: "Clients",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "Credit",
                schema: "bankdb",
                table: "Cards",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "PaymentSystem",
                schema: "bankdb",
                table: "Cards",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                schema: "bankdb",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "Credit",
                schema: "bankdb",
                table: "Cards");

            migrationBuilder.DropColumn(
                name: "PaymentSystem",
                schema: "bankdb",
                table: "Cards");
        }
    }
}
