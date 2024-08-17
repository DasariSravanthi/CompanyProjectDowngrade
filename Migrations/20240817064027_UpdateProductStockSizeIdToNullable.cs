using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CompanyApp.Migrations
{
    /// <inheritdoc />
    public partial class UpdateProductStockSizeIdToNullable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<byte>(
                name: "Size_Id",
                table: "ProductStocks",
                type: "tinyint",
                nullable: true,
                oldClrType: typeof(byte),
                oldType: "tinyint");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<byte>(
                name: "Size_Id",
                table: "ProductStocks",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)0,
                oldClrType: typeof(byte),
                oldType: "tinyint",
                oldNullable: true);
        }
    }
}
