namespace TransportationManagement.Models
{
    [Table("TB_Manufacturer")]
    public class Manufacturer
    {
        [Key]
        public int ManufacturerPkid { get; set; }

        [StringLength(100)]
        public string ManufacturerName { get; set; }

        public bool IsDeleted { get; set; }

        [NotMapped]
        public int TotalYBSNumber { get; set; }
    }
}
