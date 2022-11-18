using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Betazon.Migrations
{
    /// <inheritdoc />
    public partial class encrypt : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EncryptionData",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EncryptionAlgorithm = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EncryptedValue = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AesKey = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AesIV = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsMatch = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EncryptionData", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EncryptionData");
        }
    }
}
