using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OroSmart.Migrations
{
    /// <inheritdoc />
    public partial class WorkLocation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CustomersWorkLocations_AspNetUsers_ReferencePersonId",
                table: "CustomersWorkLocations");

            migrationBuilder.DropIndex(
                name: "IX_CustomersContacts_ContactTypeId",
                table: "CustomersContacts");

            migrationBuilder.AlterColumn<string>(
                name: "ReferencePersonId",
                table: "CustomersWorkLocations",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<int>(
                name: "ContactTypeId",
                table: "CustomersContacts",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_CustomersContacts_ContactTypeId",
                table: "CustomersContacts",
                column: "ContactTypeId",
                unique: true,
                filter: "[ContactTypeId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_CustomersWorkLocations_AspNetUsers_ReferencePersonId",
                table: "CustomersWorkLocations",
                column: "ReferencePersonId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CustomersWorkLocations_AspNetUsers_ReferencePersonId",
                table: "CustomersWorkLocations");

            migrationBuilder.DropIndex(
                name: "IX_CustomersContacts_ContactTypeId",
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

            migrationBuilder.AlterColumn<int>(
                name: "ContactTypeId",
                table: "CustomersContacts",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CustomersContacts_ContactTypeId",
                table: "CustomersContacts",
                column: "ContactTypeId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_CustomersWorkLocations_AspNetUsers_ReferencePersonId",
                table: "CustomersWorkLocations",
                column: "ReferencePersonId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
