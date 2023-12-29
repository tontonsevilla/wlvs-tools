using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WLVSTools.Web.Migrations.LottoDb
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LottoResults",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    LottoGame = table.Column<string>(type: "TEXT", nullable: true),
                    Combinations = table.Column<string>(type: "TEXT", nullable: true),
                    DrawDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    JackpotPrice = table.Column<double>(type: "REAL", nullable: true),
                    Winners = table.Column<int>(type: "INTEGER", nullable: true),
                    CreateUser = table.Column<string>(type: "TEXT", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ModifyUser = table.Column<string>(type: "TEXT", nullable: false),
                    ModifyDate = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LottoResults", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LottoResults");
        }
    }
}
