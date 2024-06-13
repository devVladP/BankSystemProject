using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BankSystem.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class CreditsAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Credit",
                schema: "bankdb",
                table: "Cards");

            migrationBuilder.AlterColumn<string>(
                name: "PaymentSystem",
                schema: "bankdb",
                table: "Cards",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Number",
                schema: "bankdb",
                table: "Cards",
                type: "nvarchar(16)",
                maxLength: 16,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "CVV2",
                schema: "bankdb",
                table: "Cards",
                type: "nvarchar(3)",
                maxLength: 3,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<decimal>(
                name: "Balance",
                schema: "bankdb",
                table: "Cards",
                type: "smallmoney",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.CreateTable(
                name: "Credits",
                schema: "bankdb",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    InitialSum = table.Column<decimal>(type: "smallmoney", nullable: false),
                    CreditIssueDate = table.Column<DateOnly>(type: "date", nullable: false),
                    PercentPerMonth = table.Column<byte>(type: "tinyint", nullable: false),
                    CardId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Credits", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Credits_Cards_CardId",
                        column: x => x.CardId,
                        principalSchema: "bankdb",
                        principalTable: "Cards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Credits_CardId",
                schema: "bankdb",
                table: "Credits",
                column: "CardId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Credits",
                schema: "bankdb");

            migrationBuilder.AlterColumn<string>(
                name: "PaymentSystem",
                schema: "bankdb",
                table: "Cards",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Number",
                schema: "bankdb",
                table: "Cards",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(16)",
                oldMaxLength: 16);

            migrationBuilder.AlterColumn<string>(
                name: "CVV2",
                schema: "bankdb",
                table: "Cards",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(3)",
                oldMaxLength: 3);

            migrationBuilder.AlterColumn<decimal>(
                name: "Balance",
                schema: "bankdb",
                table: "Cards",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "smallmoney");

            migrationBuilder.AddColumn<decimal>(
                name: "Credit",
                schema: "bankdb",
                table: "Cards",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
