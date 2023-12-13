using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OroSmart.Migrations
{
    /// <inheritdoc />
    public partial class ModifyIdentityForLastLogin : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "LastLoginIpAddress",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "LastLoginTime",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LastLoginIpAddress",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "LastLoginTime",
                table: "AspNetUsers");
        }
    }
}
