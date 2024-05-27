using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HumanResourceManagement.Migrations
{
    public partial class Add2ColumnInVehicleData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "OperatorName",
                table: "TB_VehicleData",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RegistrantOperatorName",
                table: "TB_VehicleData",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            
            migrationBuilder.DropColumn(
                name: "OperatorName",
                table: "TB_VehicleData");

            migrationBuilder.DropColumn(
                name: "RegistrantOperatorName",
                table: "TB_VehicleData");

           
        }
    }
}
