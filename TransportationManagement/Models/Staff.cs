using System.ComponentModel;

namespace TransportationManagement.Models
{
    [Table("TB_Staff")]
    public class Staff
    {
        [Key]
        public int StaffPkid { get; set; }

        [StringLength(10)]
        public string SerialNo { get; set; }

        [StringLength(50)]
        public string StaffID { get; set; }

        [StringLength(50)]
        public string Name { get; set; }

        [StringLength(50)]
        public string FatherName { get; set; }

        public DateTime? DateOfBirth { get; set; }

        [StringLength(50)]
        public string NRC { get; set; }

        [StringLength(10)]
        public string Age { get; set; }

        [StringLength(50)]
        public string Religion { get; set; }

        [StringLength(50)]
        public string VisibleMark { get; set; }

        [StringLength(300)]
        public string Address { get; set; }

        [StringLength(50)]
        public string Phone { get; set; }

        [StringLength(100)]
        public string Responsibility { get; set; }

        public DateTime? StartedDate { get; set; }

        [StringLength(50)]
        public string StaffPhoto { get; set; }

        [NotMapped]
        [DisplayName("ပုံထည့်ရန်")]
        public IFormFile ImageFile { get; set; }

        [StringLength(300)]
        public string Remarks { get; set; }

        public DateTime? CreatedDate { get; set; }
        public int? CreatedBy { get; set; }

        public bool IsDeleted { get; set; }

        [ForeignKey("Position"), DisplayName("Position")]
        public int PositionPkid { get; set; }
        public virtual Position Position { get; set; }

        [ForeignKey("Department"), DisplayName("Department")]
        public int DepartmentPkid { get; set; }
        public virtual Department Department { get; set; }


    }
}
