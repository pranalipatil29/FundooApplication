using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace FundooCommonLayer.Model
{
    public class LabelModel 
    {
        [Key]
        public int LabelID { get; set; }

        [Column(TypeName = "nvarchar(150)")]
        public string Label { get; set; }

        [Column(TypeName = "DateTime")]
        public DateTime CreatedDate { get; set; }

        [Column(TypeName = "DateTime")]
        public DateTime ModifiedDate { get; set; }
    }
}
