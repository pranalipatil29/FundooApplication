// ******************************************************************************
//  <copyright file="LabelBL.cs" company="Bridgelabz">
//    Copyright © 2019 Company
//
//     Execution:  LabelBL.cs
//  
//     Purpose:  Implementing business logic for label
//     @author  Pranali Patil
//     @version 1.0
//     @since   24-12-2019
//  </copyright>
//  <creator name="Pranali Patil"/>
// ******************************************************************************
namespace FundooBusinessLayer.ServicesBL
{
    // Including the requried assemblies in to the program
    using FundooBusinessLayer.InterfaceBL;
    using FundooCommonLayer.Model;
    using FundooCommonLayer.Model.Request;
    using FundooCommonLayer.Model.Response;
    using FundooRepositoryLayer.InterfaceRL;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    /// <summary>
    /// this class is used to check the business logic of label
    /// </summary>
    /// <seealso cref="FundooBusinessLayer.InterfaceBL.ILabelBL" />
    public class LabelBL : ILabelBL
    {
        /// <summary>
        /// creating reference of repository layer for label
        /// </summary>
        private readonly ILabelRL labelRL;

        /// <summary>
        /// Initializes a new instance of the <see cref="LabelBL"/> class.
        /// </summary>
        /// <param name="labelRL">The label rl.</param>
        public LabelBL(ILabelRL labelRL)
        {
            this.labelRL = labelRL;
        }

        /// <summary>
        /// Creates the label.
        /// </summary>
        /// <param name="labelRequest">The label request.</param>
        /// <param name="userID">The user identifier.</param>
        /// <returns></returns>
        /// <exception cref="Exception">
        /// Data Required
        /// or
        /// </exception>
        public async Task<bool> CreateLabel(LabelRequest labelRequest, string userID)
        {
           try
            {
                if(labelRequest != null)
                {
                    return await labelRL.CreateLabel(labelRequest, userID);
                }
                else
                {
                    throw new Exception("Label Data Required");
                }
            }
            catch(Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }

        /// <summary>
        /// Updates the label.
        /// </summary>
        /// <param name="labelRequest">The label request.</param>
        /// <param name="labelID">The label identifier.</param>
        /// <param name="userID">The user identifier.</param>
        /// <returns>
        /// returns the info of label
        /// </returns>
        /// <exception cref="Exception">
        /// Please enter LabelID
        /// or
        /// </exception>
        public async Task<LabelModel> UpdateLabel(LabelRequest labelRequest, int labelID, string userID)
        {
            try
            {
                if (labelID != 0)
                {
                    return await labelRL.UpdateLabel(labelRequest, labelID,userID);
                }
                else
                {
                    throw new Exception("Please enter LabelID");
                }
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }

        /// <summary>
        /// Deletes the label.
        /// </summary>
        /// <param name="labelID">The label identifier.</param>
        /// <param name="userID">The user identifier.</param>
        /// <returns>
        /// returns message indicating operation is done or not
        /// </returns>
        /// <exception cref="Exception">
        /// Pleaase enter label ID
        /// or
        /// </exception>
        public async Task<bool> DeleteLabel(int labelID, string userID)
        {
            try
            {
                if (labelID != 0)
                {
                    return await labelRL.DeleteLabel(labelID, userID);
                }
                else
                {
                    throw new Exception("Pleaase enter label ID");
                }
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }

        /// <summary>
        /// Displays the labels.
        /// </summary>
        /// <param name="userID">The user identifier.</param>
        /// <returns>
        /// returns the list of label
        /// </returns>
        /// <exception cref="Exception">
        /// User not found
        /// or
        /// </exception>
        public IList<LabelResponse> DisplayLabels( string userID)
        {
            try
            {
                if (userID != null)
                {
                    return labelRL.DisplayLabels(userID);
                }
                else
                {
                    throw new Exception("User not found");
                }
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }
    }
}
