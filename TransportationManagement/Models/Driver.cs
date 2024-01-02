using System.ComponentModel;

namespace TransportationManagement.Models
{
    [Table("TB_Driver")]
    public class Driver
    {
        [Key]
        public int DriverPkid { get; set; }

        [StringLength(50)]
        public string DriverName { get; set; }

        [StringLength(50)]
        public string DriverLicense { get; set; }

        [StringLength(50)]
        public string VehicleNumber { get; set; }
    }
}
