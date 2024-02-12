using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OroSmart.Migrations
{
    /// <inheritdoc />
    public partial class Person_id_ : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ReferencePersonId",
                table: "CustomersWorkLocations",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_CustomersWorkLocations_ReferencePersonId",
                table: "CustomersWorkLocations",
                column: "ReferencePersonId");

            migrationBuilder.AddForeignKey(
                name: "FK_CustomersWorkLocations_AspNetUsers_ReferencePersonId",
                table: "CustomersWorkLocations",
                column: "ReferencePersonId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CustomersWorkLocations_AspNetUsers_ReferencePersonId",
                table: "CustomersWorkLocations");

            migrationBuilder.DropIndex(
                name: "IX_CustomersWorkLocations_ReferencePersonId",
                table: "CustomersWorkLocations");

            migrationBuilder.DropColumn(
                name: "ReferencePersonId",
                table: "CustomersWorkLocations");
        }
    }
}
