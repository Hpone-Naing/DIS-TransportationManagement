namespace TransportationManagement.Models
{
    [Table("TB_UserType")]
    public class UserType
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserTypeID { get; set; }

        [StringLength(50)]
        public string UserTypeName { get; set; }
    }
}
