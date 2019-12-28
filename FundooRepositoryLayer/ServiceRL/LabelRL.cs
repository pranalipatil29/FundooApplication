// ******************************************************************************
//  <copyright file="LabelRL.cs" company="Bridgelabz">
//    Copyright © 2019 Company
//
//     Execution:  LabelRL.cs
//  
//     Purpose: Create, update,delete and display label
//     @author  Pranali Patil
//     @version 1.0
//     @since   14-12-2019
//  </copyright>
//  <creator name="Pranali Patil"/>
// ******************************************************************************
namespace FundooRepositoryLayer.ServiceRL
{
    // Including the requried assemblies in to the program
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using FundooCommonLayer.Model;
    using FundooCommonLayer.Model.Request;
    using FundooCommonLayer.Model.Response;
    using FundooRepositoryLayer.Context;
    using FundooRepositoryLayer.InterfaceRL;
 
    /// <summary>
    /// this class contains different methods to interact with label table
    /// </summary>
    public class LabelRL : ILabelRL
    {
        /// <summary>
        /// creating reference of authentication context class
        /// </summary>
        private AuthenticationContext authenticationContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="LabelRL"/> class.
        /// </summary>
        /// <param name="authenticationContext">The authentication context.</param>
        public LabelRL(AuthenticationContext authenticationContext)
        {
            this.authenticationContext = authenticationContext;
        }

        /// <summary>
        /// Creates the label.
        /// </summary>
        /// <param name="labelRequest">The label request.</param>
        /// <param name="userID">The user identifier.</param>
        /// <returns> returns true or false indicating operation is successful or not</returns>
        /// <exception cref="Exception"> exception message</exception>
        public async Task<bool> CreateLabel(LabelRequest labelRequest, string userID)
        {
            try
            {
                // check whether user entered all label data or not
                if (labelRequest != null)
                {
                    // set the label data values
                    var data = new LabelModel()
                    {
                        UserID = userID,
                        Label = labelRequest.Label,
                        CreatedDate = DateTime.Now,
                        ModifiedDate = DateTime.Now
                    };

                    // add the new data in tabel
                    this.authenticationContext.Label.Add(data);

                    // save the changes in database
                    await this.authenticationContext.SaveChangesAsync();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception exception)
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
        /// <exception cref="Exception"> exception message</exception>
        public async Task<LabelModel> UpdateLabel(LabelRequest labelRequest, int labelID, string userID)
        {
            try
            {
                // get the label info from label tabel
                var label = this.authenticationContext.Label.Where(s => s.LabelID == labelID && s.UserID == userID).FirstOrDefault();

                // check whether label data is null or not
                if (label != null)
                {
                    // set the current date and time for modified date property
                    label.ModifiedDate = DateTime.Now;

                    // check whether user enter label name or not
                   if (labelRequest.Label != null && labelRequest.Label != string.Empty)
                    {
                        label.Label = labelRequest.Label;
                    }

                   // update the label name
                    this.authenticationContext.Label.Update(label);
                    await this.authenticationContext.SaveChangesAsync();
                    return label;
                }
                else
                {
                    return null;
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
        /// <exception cref="Exception"> exception message</exception>
        public async Task<bool> DeleteLabel(int labelID, string userID)
        {
            try
            {
                // get the label info from label tabel
                var label = this.authenticationContext.Label.Where(s => s.LabelID == labelID && s.UserID == userID).FirstOrDefault();

                // check whether label is present in tabel or not
                if (label != null)
                {
                    // delete the label entry from tabel
                    this.authenticationContext.Label.Remove(label);
                    await this.authenticationContext.SaveChangesAsync();
                    return true;
                }
                else
                {
                    return false;
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
        /// <exception cref="Exception"> exception message</exception>
        public IList<LabelResponse> DisplayLabels(string userID)
        {
            try
            {
                // get the labels data from tabel
                var data = this.authenticationContext.Label.Where(s => s.UserID == userID);

                var list = new List<LabelResponse>();

                if (data != null)
                {
                    foreach (var label in data)
                    {
                        var labels = new LabelResponse()
                        {
                            ID = label.LabelID,
                            Label = label.Label,
                            CreatedDate = label.CreatedDate,
                            ModifiedDate = label.ModifiedDate
                        };

                        list.Add(labels);
                    }

                    return list;
                }
                else
                {
                    return null;
                }               
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }
    }
}
