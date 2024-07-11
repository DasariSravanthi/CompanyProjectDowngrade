using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CompanyApp.Migrations
{
    /// <inheritdoc />
    public partial class AllDown : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    P_Id = table.Column<byte>(type: "tinyint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Product_Category = table.Column<string>(type: "varchar(100)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.P_Id);
                });

            migrationBuilder.CreateTable(
                name: "Sizes",
                columns: table => new
                {
                    Size_Id = table.Column<byte>(type: "tinyint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Size = table.Column<short>(type: "smallint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sizes", x => x.Size_Id);
                });

            migrationBuilder.CreateTable(
                name: "Suppliers",
                columns: table => new
                {
                    S_Id = table.Column<byte>(type: "tinyint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Supplier_Name = table.Column<string>(type: "varchar(100)", nullable: false),
                    Dues = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Suppliers", x => x.S_Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "varchar(256)", nullable: false),
                    Password = table.Column<string>(type: "varchar(256)", nullable: false),
                    Email = table.Column<string>(type: "varchar(256)", nullable: false),
                    Role = table.Column<string>(type: "varchar(256)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "ProductDetails",
                columns: table => new
                {
                    PD_Id = table.Column<byte>(type: "tinyint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    P_Id = table.Column<byte>(type: "tinyint", nullable: false),
                    Variant = table.Column<string>(type: "varchar(100)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductDetails", x => x.PD_Id);
                    table.ForeignKey(
                        name: "FK_ProductDetails_Products_P_Id",
                        column: x => x.P_Id,
                        principalTable: "Products",
                        principalColumn: "P_Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Receipts",
                columns: table => new
                {
                    R_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    R_Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    S_Id = table.Column<byte>(type: "tinyint", nullable: false),
                    Bill_No = table.Column<string>(type: "varchar(100)", nullable: false),
                    Bill_Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Bill_Value = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Receipts", x => x.R_Id);
                    table.ForeignKey(
                        name: "FK_Receipts_Suppliers_S_Id",
                        column: x => x.S_Id,
                        principalTable: "Suppliers",
                        principalColumn: "S_Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductStocks",
                columns: table => new
                {
                    PS_Id = table.Column<short>(type: "smallint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PD_Id = table.Column<byte>(type: "tinyint", nullable: false),
                    GSM = table.Column<byte>(type: "tinyint", nullable: true),
                    Size_Id = table.Column<byte>(type: "tinyint", nullable: false),
                    Weight = table.Column<short>(type: "smallint", nullable: false),
                    Roll_Count = table.Column<byte>(type: "tinyint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductStocks", x => x.PS_Id);
                    table.ForeignKey(
                        name: "FK_ProductStocks_ProductDetails_PD_Id",
                        column: x => x.PD_Id,
                        principalTable: "ProductDetails",
                        principalColumn: "PD_Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductStocks_Sizes_Size_Id",
                        column: x => x.Size_Id,
                        principalTable: "Sizes",
                        principalColumn: "Size_Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ReceiptDetails",
                columns: table => new
                {
                    RD_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    R_Id = table.Column<int>(type: "int", nullable: false),
                    PS_Id = table.Column<short>(type: "smallint", nullable: false),
                    Weight = table.Column<float>(type: "real", nullable: false),
                    Unit_Rate = table.Column<float>(type: "real", nullable: false),
                    Roll_Count = table.Column<byte>(type: "tinyint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReceiptDetails", x => x.RD_Id);
                    table.ForeignKey(
                        name: "FK_ReceiptDetails_ProductStocks_PS_Id",
                        column: x => x.PS_Id,
                        principalTable: "ProductStocks",
                        principalColumn: "PS_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ReceiptDetails_Receipts_R_Id",
                        column: x => x.R_Id,
                        principalTable: "Receipts",
                        principalColumn: "R_Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RollNumbers",
                columns: table => new
                {
                    RN_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RD_Id = table.Column<int>(type: "int", nullable: false),
                    Roll_Number = table.Column<string>(type: "varchar(100)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RollNumbers", x => x.RN_Id);
                    table.ForeignKey(
                        name: "FK_RollNumbers_ReceiptDetails_RD_Id",
                        column: x => x.RD_Id,
                        principalTable: "ReceiptDetails",
                        principalColumn: "RD_Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Issues",
                columns: table => new
                {
                    Issue_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    I_Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RN_Id = table.Column<int>(type: "int", nullable: false),
                    PS_Id = table.Column<short>(type: "smallint", nullable: false),
                    Roll_No = table.Column<string>(type: "varchar(100)", nullable: true),
                    Weight = table.Column<float>(type: "real", nullable: false),
                    Moisture = table.Column<float>(type: "real", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Issues", x => x.Issue_Id);
                    table.ForeignKey(
                        name: "FK_Issues_ProductStocks_PS_Id",
                        column: x => x.PS_Id,
                        principalTable: "ProductStocks",
                        principalColumn: "PS_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Issues_RollNumbers_RN_Id",
                        column: x => x.RN_Id,
                        principalTable: "RollNumbers",
                        principalColumn: "RN_Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductionCoatings",
                columns: table => new
                {
                    P_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    P_Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Issue_Id = table.Column<int>(type: "int", nullable: false),
                    Coating_Start = table.Column<TimeSpan>(type: "time", nullable: false),
                    Coating_End = table.Column<TimeSpan>(type: "time", nullable: false),
                    Avg_Speed = table.Column<byte>(type: "tinyint", nullable: false),
                    Avg_Temperature = table.Column<byte>(type: "tinyint", nullable: false),
                    GSM_Coated = table.Column<byte>(type: "tinyint", nullable: false),
                    Roll_Count = table.Column<byte>(type: "tinyint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductionCoatings", x => x.P_Id);
                    table.ForeignKey(
                        name: "FK_ProductionCoatings_Issues_Issue_Id",
                        column: x => x.Issue_Id,
                        principalTable: "Issues",
                        principalColumn: "Issue_Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductionCalendarings",
                columns: table => new
                {
                    PC_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    P_Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    P_Id = table.Column<int>(type: "int", nullable: false),
                    Roll_No = table.Column<string>(type: "varchar(100)", nullable: false),
                    Before_Weight = table.Column<float>(type: "real", nullable: false),
                    Before_Moisture = table.Column<float>(type: "real", nullable: false),
                    Calendaring_Start = table.Column<TimeSpan>(type: "time", nullable: false),
                    Calendaring_End = table.Column<TimeSpan>(type: "time", nullable: false),
                    Roll_Count = table.Column<byte>(type: "tinyint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductionCalendarings", x => x.PC_Id);
                    table.ForeignKey(
                        name: "FK_ProductionCalendarings_ProductionCoatings_P_Id",
                        column: x => x.P_Id,
                        principalTable: "ProductionCoatings",
                        principalColumn: "P_Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductionSlittings",
                columns: table => new
                {
                    PS_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    P_Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PC_Id = table.Column<int>(type: "int", nullable: false),
                    Roll_No = table.Column<string>(type: "varchar(100)", nullable: false),
                    Before_Weight = table.Column<float>(type: "real", nullable: false),
                    Before_Moisture = table.Column<float>(type: "real", nullable: false),
                    Slitting_Start = table.Column<TimeSpan>(type: "time", nullable: false),
                    Slitting_End = table.Column<TimeSpan>(type: "time", nullable: false),
                    Roll_Count = table.Column<byte>(type: "tinyint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductionSlittings", x => x.PS_Id);
                    table.ForeignKey(
                        name: "FK_ProductionSlittings_ProductionCalendarings_PC_Id",
                        column: x => x.PC_Id,
                        principalTable: "ProductionCalendarings",
                        principalColumn: "PC_Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SlittingDetails",
                columns: table => new
                {
                    SD_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PS_Id = table.Column<int>(type: "int", nullable: false),
                    Roll_No = table.Column<string>(type: "varchar(100)", nullable: false),
                    Weight = table.Column<float>(type: "real", nullable: false),
                    Moisture = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SlittingDetails", x => x.SD_Id);
                    table.ForeignKey(
                        name: "FK_SlittingDetails_ProductionSlittings_PS_Id",
                        column: x => x.PS_Id,
                        principalTable: "ProductionSlittings",
                        principalColumn: "PS_Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Issues_PS_Id",
                table: "Issues",
                column: "PS_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Issues_RN_Id",
                table: "Issues",
                column: "RN_Id");

            migrationBuilder.CreateIndex(
                name: "IX_ProductDetails_P_Id",
                table: "ProductDetails",
                column: "P_Id");

            migrationBuilder.CreateIndex(
                name: "IX_ProductionCalendarings_P_Id",
                table: "ProductionCalendarings",
                column: "P_Id");

            migrationBuilder.CreateIndex(
                name: "IX_ProductionCoatings_Issue_Id",
                table: "ProductionCoatings",
                column: "Issue_Id");

            migrationBuilder.CreateIndex(
                name: "IX_ProductionSlittings_PC_Id",
                table: "ProductionSlittings",
                column: "PC_Id");

            migrationBuilder.CreateIndex(
                name: "IX_ProductStocks_PD_Id",
                table: "ProductStocks",
                column: "PD_Id");

            migrationBuilder.CreateIndex(
                name: "IX_ProductStocks_Size_Id",
                table: "ProductStocks",
                column: "Size_Id");

            migrationBuilder.CreateIndex(
                name: "IX_ReceiptDetails_PS_Id",
                table: "ReceiptDetails",
                column: "PS_Id");

            migrationBuilder.CreateIndex(
                name: "IX_ReceiptDetails_R_Id",
                table: "ReceiptDetails",
                column: "R_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Receipts_S_Id",
                table: "Receipts",
                column: "S_Id");

            migrationBuilder.CreateIndex(
                name: "IX_RollNumbers_RD_Id",
                table: "RollNumbers",
                column: "RD_Id");

            migrationBuilder.CreateIndex(
                name: "IX_SlittingDetails_PS_Id",
                table: "SlittingDetails",
                column: "PS_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_Username",
                table: "Users",
                column: "Username",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SlittingDetails");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "ProductionSlittings");

            migrationBuilder.DropTable(
                name: "ProductionCalendarings");

            migrationBuilder.DropTable(
                name: "ProductionCoatings");

            migrationBuilder.DropTable(
                name: "Issues");

            migrationBuilder.DropTable(
                name: "RollNumbers");

            migrationBuilder.DropTable(
                name: "ReceiptDetails");

            migrationBuilder.DropTable(
                name: "ProductStocks");

            migrationBuilder.DropTable(
                name: "Receipts");

            migrationBuilder.DropTable(
                name: "ProductDetails");

            migrationBuilder.DropTable(
                name: "Sizes");

            migrationBuilder.DropTable(
                name: "Suppliers");

            migrationBuilder.DropTable(
                name: "Products");
        }
    }
}
