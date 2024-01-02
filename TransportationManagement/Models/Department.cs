namespace TransportationManagement.Models
{
    [Table("TB_Department")]
    public class Department
    {
        [Key]
        public int DepartmentPkid { get; set; }

        [StringLength(200)]
        public string DepartmentName { get; set; }
    }
}
