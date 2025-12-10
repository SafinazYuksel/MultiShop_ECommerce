using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MultiShop.Payment.Migrations
{
    /// <inheritdoc />
    public partial class init2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PaymentDate",
                table: "PaymentDetails");

            migrationBuilder.RenameColumn(
                name: "TransactionId",
                table: "PaymentDetails",
                newName: "ExpirationYear");

            migrationBuilder.RenameColumn(
                name: "PaymentStatus",
                table: "PaymentDetails",
                newName: "ExpirationMonth");

            migrationBuilder.AddColumn<string>(
                name: "CVV",
                table: "PaymentDetails",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CardHolderName",
                table: "PaymentDetails",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CardNumber",
                table: "PaymentDetails",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CVV",
                table: "PaymentDetails");

            migrationBuilder.DropColumn(
                name: "CardHolderName",
                table: "PaymentDetails");

            migrationBuilder.DropColumn(
                name: "CardNumber",
                table: "PaymentDetails");

            migrationBuilder.RenameColumn(
                name: "ExpirationYear",
                table: "PaymentDetails",
                newName: "TransactionId");

            migrationBuilder.RenameColumn(
                name: "ExpirationMonth",
                table: "PaymentDetails",
                newName: "PaymentStatus");

            migrationBuilder.AddColumn<DateTime>(
                name: "PaymentDate",
                table: "PaymentDetails",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
