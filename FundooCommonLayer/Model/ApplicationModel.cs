using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace FundooCommonLayer.Model
{
    public class ApplicationModel:IdentityUser
    {
        [Column(TypeName = "nvarchar(150)")]
        public string FirstName { get; set; }

        [Column(TypeName = "nvarchar(150)")]
        public string LastName { get; set; }
   
        public bool IsFacebook { get; set; }

        public bool IsGoogle { get; set; }

        [Column(TypeName = "int")]
        public int UserType { get; set; }

        [ForeignKey("ServiceModel")]
        public string ServiceType { get; set; }
    }
}
