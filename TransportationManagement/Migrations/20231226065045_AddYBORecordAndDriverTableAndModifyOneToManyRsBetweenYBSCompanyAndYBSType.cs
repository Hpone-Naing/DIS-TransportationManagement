using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HumanResourceManagement.Migrations
{
    public partial class AddYBORecordAndDriverTableAndModifyOneToManyRsBetweenYBSCompanyAndYBSType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "YBSCompanyPkid",
                table: "TB_YBSType",
                type: "int",
                nullable: true,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "TB_Driver",
                columns: table => new
                {
                    DriverPkid = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DriverName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    DriverLicense = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_Driver", x => x.DriverPkid);
                });

            migrationBuilder.CreateTable(
                name: "TB_YboRecord",
                columns: table => new
                {
                    YboRecordPkid = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RecordDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RecordTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TotalRecord = table.Column<int>(type: "int", nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    YbsRecordSender = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    RecordDescription = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    CompletionStatus = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CompletedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    YBSCompanyPkid = table.Column<int>(type: "int", nullable: false),
                    YBSTypePkid = table.Column<int>(type: "int", nullable: false),
                    DriverPkid = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_YboRecord", x => x.YboRecordPkid);
                    table.ForeignKey(
                        name: "FK_TB_YboRecord_TB_Driver_DriverPkid",
                        column: x => x.DriverPkid,
                        principalTable: "TB_Driver",
                        principalColumn: "DriverPkid",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TB_YboRecord_TB_YBSCompany_YBSCompanyPkid",
                        column: x => x.YBSCompanyPkid,
                        principalTable: "TB_YBSCompany",
                        principalColumn: "YBSCompanyPkid",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TB_YboRecord_TB_YBSType_YBSTypePkid",
                        column: x => x.YBSTypePkid,
                        principalTable: "TB_YBSType",
                        principalColumn: "YBSTypePkid",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TB_YBSType_YBSCompanyPkid",
                table: "TB_YBSType",
                column: "YBSCompanyPkid");

            migrationBuilder.CreateIndex(
                name: "IX_TB_YboRecord_DriverPkid",
                table: "TB_YboRecord",
                column: "DriverPkid");

            migrationBuilder.CreateIndex(
                name: "IX_TB_YboRecord_YBSCompanyPkid",
                table: "TB_YboRecord",
                column: "YBSCompanyPkid");

            migrationBuilder.CreateIndex(
                name: "IX_TB_YboRecord_YBSTypePkid",
                table: "TB_YboRecord",
                column: "YBSTypePkid");

            migrationBuilder.AddForeignKey(
                name: "FK_TB_YBSType_TB_YBSCompany_YBSCompanyPkid",
                table: "TB_YBSType",
                column: "YBSCompanyPkid",
                principalTable: "TB_YBSCompany",
                principalColumn: "YBSCompanyPkid",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TB_YBSType_TB_YBSCompany_YBSCompanyPkid",
                table: "TB_YBSType");

            migrationBuilder.DropTable(
                name: "TB_YboRecord");

            migrationBuilder.DropTable(
                name: "TB_Driver");

            migrationBuilder.DropIndex(
                name: "IX_TB_YBSType_YBSCompanyPkid",
                table: "TB_YBSType");

            migrationBuilder.DropColumn(
                name: "YBSCompanyPkid",
                table: "TB_YBSType");
        }
    }
}
