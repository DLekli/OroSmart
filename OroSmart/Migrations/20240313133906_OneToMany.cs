using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OroSmart.Migrations
{
    /// <inheritdoc />
    public partial class OneToMany : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_CustomersContacts_ContactTypeId",
                table: "CustomersContacts");

            migrationBuilder.DropIndex(
                name: "IX_CustomersContacts_CustomerId",
                table: "CustomersContacts");

            migrationBuilder.AlterColumn<string>(
                name: "ReferencePersonId",
                table: "CustomersWorkLocations",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ContactTypeId1",
                table: "CustomersContacts",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CustomersContacts_ContactTypeId",
                table: "CustomersContacts",
                column: "ContactTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomersContacts_ContactTypeId1",
                table: "CustomersContacts",
                column: "ContactTypeId1");

            migrationBuilder.CreateIndex(
                name: "IX_CustomersContacts_CustomerId",
                table: "CustomersContacts",
                column: "CustomerId");

            migrationBuilder.AddForeignKey(
                name: "FK_CustomersContacts_ContactTypes_ContactTypeId1",
                table: "CustomersContacts",
                column: "ContactTypeId1",
                principalTable: "ContactTypes",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CustomersContacts_ContactTypes_ContactTypeId1",
                table: "CustomersContacts");

            migrationBuilder.DropIndex(
                name: "IX_CustomersContacts_ContactTypeId",
                table: "CustomersContacts");

            migrationBuilder.DropIndex(
                name: "IX_CustomersContacts_ContactTypeId1",
                table: "CustomersContacts");

            migrationBuilder.DropIndex(
                name: "IX_CustomersContacts_CustomerId",
                table: "CustomersContacts");

            migrationBuilder.DropColumn(
                name: "ContactTypeId1",
                table: "CustomersContacts");

            migrationBuilder.AlterColumn<string>(
                name: "ReferencePersonId",
                table: "CustomersWorkLocations",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.CreateIndex(
                name: "IX_CustomersContacts_ContactTypeId",
                table: "CustomersContacts",
                column: "ContactTypeId",
                unique: true,
                filter: "[ContactTypeId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_CustomersContacts_CustomerId",
                table: "CustomersContacts",
                column: "CustomerId",
                unique: true);
        }
    }
}
