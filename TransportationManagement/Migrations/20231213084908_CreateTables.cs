using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HumanResourceManagement.Migrations
{
    public partial class CreateTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TB_Department",
                columns: table => new
                {
                    DepartmentPkid = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DepartmentName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_Department", x => x.DepartmentPkid);
                });

            migrationBuilder.CreateTable(
                name: "TB_FuelType",
                columns: table => new
                {
                    FuelTypePkid = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FuelTypeName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_FuelType", x => x.FuelTypePkid);
                });

            migrationBuilder.CreateTable(
                name: "TB_Manufacturer",
                columns: table => new
                {
                    ManufacturerPkid = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ManufacturerName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_Manufacturer", x => x.ManufacturerPkid);
                });

            migrationBuilder.CreateTable(
                name: "TB_YBSCompany",
                columns: table => new
                {
                    YBSCompanyPkid = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    YBSCompanyName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_YBSCompany", x => x.YBSCompanyPkid);
                });

            migrationBuilder.CreateTable(
                name: "TB_YBSType",
                columns: table => new
                {
                    YBSTypePkid = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    YBSTypeName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_YBSType", x => x.YBSTypePkid);
                });

            migrationBuilder.CreateTable(
                name: "TB_Position",
                columns: table => new
                {
                    PositionPkid = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PositionName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    DepartmentPkid = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_Position", x => x.PositionPkid);
                    table.ForeignKey(
                        name: "FK_TB_Position_TB_Department_DepartmentPkid",
                        column: x => x.DepartmentPkid,
                        principalTable: "TB_Department",
                        principalColumn: "DepartmentPkid",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TB_VehicleData",
                columns: table => new
                {
                    VehicleDataPkid = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SerialNo = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    YBSName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    VehicleNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ManufacturedYear = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CngQty = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CctvInstalled = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    AssignedRoute = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    TelematicDeviceInstalled = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    TotalBusStop = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    POSInstalled = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    RegistrationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Remarks = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    YBSCompanyPkid = table.Column<int>(type: "int", nullable: false),
                    VehicleTypePkid = table.Column<int>(type: "int", nullable: false),
                    FuelTypePkid = table.Column<int>(type: "int", nullable: false),
                    VehicleManufacturer = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_VehicleData", x => x.VehicleDataPkid);
                    table.ForeignKey(
                        name: "FK_TB_VehicleData_TB_FuelType_FuelTypePkid",
                        column: x => x.FuelTypePkid,
                        principalTable: "TB_FuelType",
                        principalColumn: "FuelTypePkid",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TB_VehicleData_TB_Manufacturer_VehicleManufacturer",
                        column: x => x.VehicleManufacturer,
                        principalTable: "TB_Manufacturer",
                        principalColumn: "ManufacturerPkid");
                    table.ForeignKey(
                        name: "FK_TB_VehicleData_TB_YBSCompany_YBSCompanyPkid",
                        column: x => x.YBSCompanyPkid,
                        principalTable: "TB_YBSCompany",
                        principalColumn: "YBSCompanyPkid",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TB_VehicleData_TB_YBSType_VehicleTypePkid",
                        column: x => x.VehicleTypePkid,
                        principalTable: "TB_YBSType",
                        principalColumn: "YBSTypePkid",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TB_Staff",
                columns: table => new
                {
                    StaffPkid = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SerialNo = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    StaffID = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    FatherName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: true),
                    NRC = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Age = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    Religion = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    VisibleMark = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Address = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Responsibility = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    StartedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Remarks = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    PositionPkid = table.Column<int>(type: "int", nullable: false),
                    DepartmentPkid = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_Staff", x => x.StaffPkid);
                    table.ForeignKey(
                        name: "FK_TB_Staff_TB_Department_DepartmentPkid",
                        column: x => x.DepartmentPkid,
                        principalTable: "TB_Department",
                        principalColumn: "DepartmentPkid",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TB_Staff_TB_Position_PositionPkid",
                        column: x => x.PositionPkid,
                        principalTable: "TB_Position",
                        principalColumn: "PositionPkid",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TB_Position_DepartmentPkid",
                table: "TB_Position",
                column: "DepartmentPkid");

            migrationBuilder.CreateIndex(
                name: "IX_TB_Staff_DepartmentPkid",
                table: "TB_Staff",
                column: "DepartmentPkid");

            migrationBuilder.CreateIndex(
                name: "IX_TB_Staff_PositionPkid",
                table: "TB_Staff",
                column: "PositionPkid");

            migrationBuilder.CreateIndex(
                name: "IX_TB_VehicleData_FuelTypePkid",
                table: "TB_VehicleData",
                column: "FuelTypePkid");

            migrationBuilder.CreateIndex(
                name: "IX_TB_VehicleData_VehicleManufacturer",
                table: "TB_VehicleData",
                column: "VehicleManufacturer");

            migrationBuilder.CreateIndex(
                name: "IX_TB_VehicleData_VehicleTypePkid",
                table: "TB_VehicleData",
                column: "VehicleTypePkid");

            migrationBuilder.CreateIndex(
                name: "IX_TB_VehicleData_YBSCompanyPkid",
                table: "TB_VehicleData",
                column: "YBSCompanyPkid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TB_Staff");

            migrationBuilder.DropTable(
                name: "TB_VehicleData");

            migrationBuilder.DropTable(
                name: "TB_Position");

            migrationBuilder.DropTable(
                name: "TB_FuelType");

            migrationBuilder.DropTable(
                name: "TB_Manufacturer");

            migrationBuilder.DropTable(
                name: "TB_YBSCompany");

            migrationBuilder.DropTable(
                name: "TB_YBSType");

            migrationBuilder.DropTable(
                name: "TB_Department");
        }
    }
}
