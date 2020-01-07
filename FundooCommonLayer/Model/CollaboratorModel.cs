// ******************************************************************************
//  <copyright file="CollaboratorModel.cs" company="Bridgelabz">
//    Copyright © 2019 Company
//
//     Execution:  CollaboratorModel.cs
//  
//     Purpose:  Creating columns for collaborator table
//     @author  Pranali Patil
//     @version 1.0
//     @since  6-1-2020
//  </copyright>
//  <creator name="Pranali Patil"/>
// ******************************************************************************
namespace FundooCommonLayer.Model
{
    // Including the requried assemblies in to the program
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Text;

    public class CollaboratorModel
    {
        [Key]
        public int ID { get; set; }

        [ForeignKey("NoteModel")]
        public int NoteID { get; set; }

        [ForeignKey("RegistrationModel")]
        public string UserID { get; set; }

        [Column (TypeName ="nvarchar(150)")]
        public string CollaboratorID { get; set; }

        [Column(TypeName = "nvarchar(150)")]
        public string EmailID { get; set; }

        [Column(TypeName = "DateTime")]
        public DateTime CreatedDate { get; set; }

        [Column(TypeName = "DateTime")]
        public DateTime ModifiedDate { get; set; }
    }
}
