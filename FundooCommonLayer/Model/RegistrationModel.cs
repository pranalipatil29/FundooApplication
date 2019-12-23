using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FundooCommonLayer.Model
{
    public class RegistrationModel
    {
        [Key]
        public string Id { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        public string EmailID { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public int UserType { get; set; }

        public bool IsFacebook { get; set; }

        [Required]
        public string ServiceType { get; set; }
    }
}
