using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FundooCommonLayer.Model.Request.Note
{
   public class ReminderRequest
    {
        [Required]
        public DateTime Reminder { get; set; }
    }
}
