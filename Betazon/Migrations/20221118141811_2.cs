using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Betazon.Migrations
{
    /// <inheritdoc />
    public partial class _2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Admin_EncryptionData_EncryptionDataId",
                table: "Admin");

            migrationBuilder.AlterColumn<int>(
                name: "EncryptionDataId",
                table: "Admin",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Admin_EncryptionData_EncryptionDataId",
                table: "Admin",
                column: "EncryptionDataId",
                principalTable: "EncryptionData",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Admin_EncryptionData_EncryptionDataId",
                table: "Admin");

            migrationBuilder.AlterColumn<int>(
                name: "EncryptionDataId",
                table: "Admin",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Admin_EncryptionData_EncryptionDataId",
                table: "Admin",
                column: "EncryptionDataId",
                principalTable: "EncryptionData",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
