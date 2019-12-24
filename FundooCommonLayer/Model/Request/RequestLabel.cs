using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FundooCommonLayer.Model.Request
{
   public class RequestLabel
    {
        [Required]
        public string Label { get; set; }
    }
}
