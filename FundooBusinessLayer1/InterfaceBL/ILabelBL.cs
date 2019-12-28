// ******************************************************************************
//  <copyright file="ILabelBL.cs" company="Bridgelabz">
//    Copyright © 2019 Company
//
//     Execution:  ILabelBL.cs
//  
//     Purpose:  Creating label interface for business layer
//     @author  Pranali Patil
//     @version 1.0
//     @since   24-12-2019
//  </copyright>
//  <creator name="Pranali Patil"/>
// ******************************************************************************
namespace FundooBusinessLayer.InterfaceBL
{
    // Including the requried assemblies in to the program
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;
    using FundooCommonLayer.Model;
    using FundooCommonLayer.Model.Request;
    using FundooCommonLayer.Model.Response;

    /// <summary>
    ///  creating Label interface for business layer
    /// </summary>
    public interface ILabelBL
    {
        /// <summary>
        /// Creates the label.
        /// </summary>
        /// <param name="requestLabel">The request label.</param>
        /// <param name="userID">The user identifier.</param>
        /// <returns>returns message indicating operation is done or not</returns>
        Task<bool> CreateLabel(LabelRequest requestLabel, string userID);

        /// <summary>
        /// Updates the label.
        /// </summary>
        /// <param name="labelRequest">The label request.</param>
        /// <param name="labelID">The label identifier.</param>
        /// <param name="userID">The user identifier.</param>
        /// <returns> returns the info of label</returns>
        Task<LabelModel> UpdateLabel(LabelRequest labelRequest, int labelID, string userID);

        /// <summary>
        /// Displays the labels.
        /// </summary>
        /// <param name="userID">The user identifier.</param>
        /// <returns>returns the list of label</returns>
        IList<LabelResponse> DisplayLabels(string userID);

        /// <summary>
        /// Deletes the label.
        /// </summary>
        /// <param name="labelID">The label identifier.</param>
        /// <param name="userID">The user identifier.</param>
        /// <returns>returns message indicating operation is done or not</returns>
        Task<bool> DeleteLabel(int labelID, string userID);
    }
}
