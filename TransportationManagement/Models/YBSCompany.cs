namespace TransportationManagement.Models
{
    [Table("TB_YBSCompany")]
    public class YBSCompany
    {
        [Key]
        public int YBSCompanyPkid { get; set; }

        [StringLength(100)]
        public string YBSCompanyName { get; set; }

        [StringLength(100)]
        public string? OwnerName { get; set; }

        [StringLength(100)]
        public string? Address { get; set; }

        [StringLength(100)]
        public string? PhoneNumber { get; set; }

        public bool IsDeleted { get; set; }

        [NotMapped]
        public int TotalYBSNumber { get; set; }
    }
}
