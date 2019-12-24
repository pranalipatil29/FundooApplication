using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FundooCommonLayer.Model.Request
{
   public class RequestNote
    {
       
        public string Title { get; set; }
        
        public string Description { get; set; }
   
        [Required]
        public DateTime Reminder { get; set; }
      
        public string Collaborator { get; set; }
       
        public string Color { get; set; }
       
        public bool IsArchive { get; set; }
      
        public bool IsPin { get; set; }
        
        public bool IsTrash { get; set; }
       
        public string Image { get; set; }

    }
}
