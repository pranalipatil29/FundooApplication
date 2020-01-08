// ******************************************************************************
//  <copyright file="CollaboratorRsponse.cs" company="Bridgelabz">
//    Copyright © 2019 Company
//
//     Execution:  CollaboratorRsponse.cs
//  
//     Purpose:  Defining properties for send response of collaborators
//     @author  Pranali Patil
//     @version 1.0
//     @since   24-12-2019
//  </copyright>
//  <creator name="Pranali Patil"/>
// ******************************************************************************
namespace FundooCommonLayer.Model.Response.Note
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    /// <summary>
    /// this class contains properties of collaborator table which are send as response
    /// </summary>
    public class CollaboratorRsponse
    {
        /// <summary>
        /// Gets or sets the collaborator identifier.
        /// </summary>
        /// <value>
        /// The collaborator identifier.
        /// </value>
        public string CollaboratorID { get; set; }

        /// <summary>
        /// Gets or sets the email identifier.
        /// </summary>
        /// <value>
        /// The email identifier.
        /// </value>
        public string EmailID { get; set; }
    }
}
