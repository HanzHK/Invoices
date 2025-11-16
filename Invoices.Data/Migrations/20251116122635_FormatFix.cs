using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Invoices.Data.Migrations
{
    /// <inheritdoc />
    public partial class FormatFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Invoices_Persons_BuyerPersonId",
                table: "Invoices");

            migrationBuilder.DropForeignKey(
                name: "FK_Invoices_Persons_SellerPersonId",
                table: "Invoices");

            migrationBuilder.RenameColumn(
                name: "SellerPersonId",
                table: "Invoices",
                newName: "SellerId");

            migrationBuilder.RenameColumn(
                name: "BuyerPersonId",
                table: "Invoices",
                newName: "BuyerId");

            migrationBuilder.RenameIndex(
                name: "IX_Invoices_SellerPersonId",
                table: "Invoices",
                newName: "IX_Invoices_SellerId");

            migrationBuilder.RenameIndex(
                name: "IX_Invoices_BuyerPersonId",
                table: "Invoices",
                newName: "IX_Invoices_BuyerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Invoices_Persons_BuyerId",
                table: "Invoices",
                column: "BuyerId",
                principalTable: "Persons",
                principalColumn: "PersonId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Invoices_Persons_SellerId",
                table: "Invoices",
                column: "SellerId",
                principalTable: "Persons",
                principalColumn: "PersonId",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Invoices_Persons_BuyerId",
                table: "Invoices");

            migrationBuilder.DropForeignKey(
                name: "FK_Invoices_Persons_SellerId",
                table: "Invoices");

            migrationBuilder.RenameColumn(
                name: "SellerId",
                table: "Invoices",
                newName: "SellerPersonId");

            migrationBuilder.RenameColumn(
                name: "BuyerId",
                table: "Invoices",
                newName: "BuyerPersonId");

            migrationBuilder.RenameIndex(
                name: "IX_Invoices_SellerId",
                table: "Invoices",
                newName: "IX_Invoices_SellerPersonId");

            migrationBuilder.RenameIndex(
                name: "IX_Invoices_BuyerId",
                table: "Invoices",
                newName: "IX_Invoices_BuyerPersonId");

            migrationBuilder.AddForeignKey(
                name: "FK_Invoices_Persons_BuyerPersonId",
                table: "Invoices",
                column: "BuyerPersonId",
                principalTable: "Persons",
                principalColumn: "PersonId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Invoices_Persons_SellerPersonId",
                table: "Invoices",
                column: "SellerPersonId",
                principalTable: "Persons",
                principalColumn: "PersonId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
