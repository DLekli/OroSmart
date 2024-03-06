using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OroSmart.Migrations
{
    /// <inheritdoc />
    public partial class Contact : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ContactTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactTypes", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CustomersContacts_ContactTypeId",
                table: "CustomersContacts",
                column: "ContactTypeId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_CustomersContacts_ContactTypes_ContactTypeId",
                table: "CustomersContacts",
                column: "ContactTypeId",
                principalTable: "ContactTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CustomersContacts_ContactTypes_ContactTypeId",
                table: "CustomersContacts");

            migrationBuilder.DropTable(
                name: "ContactTypes");

            migrationBuilder.DropIndex(
                name: "IX_CustomersContacts_ContactTypeId",
                table: "CustomersContacts");
        }
    }
}
