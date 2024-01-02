﻿using System.ComponentModel;

namespace TransportationManagement.Models
{
    [Table("TB_YBSType")]
    public class YBSType
    {
        [Key]
        public int YBSTypePkid { get; set; }

        [StringLength(50)]
        public string YBSTypeName { get; set; }

        [ForeignKey("YBSCompany")]
        public int YBSCompanyPkid { get; set; }
        public virtual YBSCompany YBSCompany { get; set; }
    }
}
