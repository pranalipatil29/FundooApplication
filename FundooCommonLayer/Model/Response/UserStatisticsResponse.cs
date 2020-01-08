// ******************************************************************************
//  <copyright file="UserStatisticsResponse.cs" company="Bridgelabz">
//    Copyright © 2019 Company
//
//     Execution:  UserStatisticsResponse.cs
//  
//     Purpose:  Defining properties for handling User statistics response
//     @author  Pranali Patil
//     @version 1.0
//     @since   4-1-2020
//  </copyright>
//  <creator name="Pranali Patil"/>
// ******************************************************************************
namespace FundooCommonLayer.Model.Response
{
    /// <summary>
    /// this class contains properties for user Statistics Response
    /// </summary>
    public class UserStatisticsResponse
    {
        /// <summary>
        /// Gets or sets the basic.
        /// </summary>
        /// <value>
        /// The basic.
        /// </value>
        public int Basic { get; set; }

        /// <summary>
        /// Gets or sets the advance.
        /// </summary>
        /// <value>
        /// The advance.
        /// </value>
        public int Advance { get; set; }
    }
}
