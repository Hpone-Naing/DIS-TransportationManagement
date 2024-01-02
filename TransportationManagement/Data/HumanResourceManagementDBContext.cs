using Microsoft.EntityFrameworkCore;
using TransportationManagement.Models;

namespace TransportationManagement.Data
{
    public class HumanResourceManagementDBContext : DbContext
    {
        public HumanResourceManagementDBContext(DbContextOptions<HumanResourceManagementDBContext> options) : base(options)
        {

        }

        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Department> Departments { get; set; }
        public virtual DbSet<FuelType> FuelTypes { get; set; }
        public virtual DbSet<Manufacturer> Manufacturers { get; set; }
        public virtual DbSet<Position> Positions { get; set; }
        public virtual DbSet<Staff> Staffs { get; set; }
        public virtual DbSet<VehicleData> VehicleDatas { get; set; }
        public virtual DbSet<YBSCompany> YBSCompanies { get; set; }
        public virtual DbSet<YBSType> YBSTypes { get; set; }
        public virtual DbSet<YboRecord> YboRecords { get; set; }
        public virtual DbSet<Driver> Drivers { get; set; }


    }
}
