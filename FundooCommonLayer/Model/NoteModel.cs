using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace FundooCommonLayer.Model
{
    public class NoteModel
    {
        [Key]
        public int NoteID { get; set; }

        [ForeignKey("RegistrationModel")]
        public string UserID { get; set; }

        [Column(TypeName = "nvarchar(150)")]
        public string Title { get; set; }

        [Column(TypeName = "nvarchar(150)")]
        public string Description { get; set; }

        [Column(TypeName = "DateTime")]
        public DateTime Reminder { get; set; }

        [Column(TypeName = "nvarchar(150)")]
        public string Collaborator { get; set; }

        [Column(TypeName = "nvarchar(150)")]
        public string Color { get; set; }

       // [Column(TypeName = "int")]
        public bool IsArchive { get; set; }

        // [Column(TypeName = "int")]
        public bool IsPin { get; set; }

        // [Column(TypeName = "int")]
        public bool IsTrash { get; set; }

        [Column(TypeName = "nvarchar(150)")]
        public string Image { get; set; }

        [Column(TypeName = "DateTime")]
        public DateTime CreatedDate { get; set; }

        [Column(TypeName = "DateTime")]
        public DateTime ModifiedDate { get; set; }
    }
}
