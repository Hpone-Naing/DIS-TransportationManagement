using System.ComponentModel;

namespace TransportationManagement.Models
{
    [Table("TB_Position")]
    public class Position
    {
        [Key]
        public int PositionPkid { get; set; }

        [StringLength(50)]
        public string PositionName { get; set; }

        [ForeignKey("Department"), DisplayName("Department")]
        public int DepartmentPkid { get; set; }
        public virtual Department Department { get; set; }


    }
}
