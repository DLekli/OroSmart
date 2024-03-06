using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OroSmart.Migrations
{
    /// <inheritdoc />
    public partial class ContactType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CustomersContacts_ContactTypes_ContactTypeId",
                table: "CustomersContacts");

            migrationBuilder.AddForeignKey(
                name: "FK_CustomersContacts_ContactTypes_ContactTypeId",
                table: "CustomersContacts",
                column: "ContactTypeId",
                principalTable: "ContactTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CustomersContacts_ContactTypes_ContactTypeId",
                table: "CustomersContacts");

            migrationBuilder.AddForeignKey(
                name: "FK_CustomersContacts_ContactTypes_ContactTypeId",
                table: "CustomersContacts",
                column: "ContactTypeId",
                principalTable: "ContactTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
