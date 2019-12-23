using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace FundooCommonLayer.Model
{
   public class NoteLabelModel
    {
        [Key]
        public int ID { get; set; }

        [ForeignKey("RegistrationModel")]
        public int UserID { get; set; }

        [ForeignKey("NoteModel")]
        public int NoteID { get; set; }

        [ForeignKey("LableModel")]
        public int LabelID { get; set; }

        [Column(TypeName ="nvarchar(150)")]
        public string Label { get; set; }
    }
}
