// ******************************************************************************
//  <copyright file="CollaboratorRequest.cs" company="Bridgelabz">
//    Copyright © 2019 Company
//
//     Execution:  CollaboratorRequest.cs
//  
//     Purpose:  Request for note collaborator info
//     @author  Pranali Patil
//     @version 1.0
//     @since  6-1-2020
//  </copyright>
//  <creator name="Pranali Patil"/>
// ******************************************************************************
namespace FundooCommonLayer.Model.Request.Note
{
    // Including the requried assemblies in to the program
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    /// <summary>
    /// this class contains properties for note Collaborator
    /// </summary>
    public class CollaboratorRequest
    {
         /// <summary>
        /// The email identifier
        /// </summary>
        public string ID;
    }
}
