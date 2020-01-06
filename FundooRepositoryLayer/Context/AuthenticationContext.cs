// ******************************************************************************
//  <copyright file="AuthenticationContext.cs" company="Bridgelabz">
//    Copyright © 2019 Company
//
//     Execution:  AuthenticationContext.cs
//  
//     Purpose:  Creating database connection
//     @author  Pranali Patil
//     @version 1.0
//     @since   14-12-2019
//  </copyright>
//  <creator name="Pranali Patil"/>
// ******************************************************************************
namespace FundooRepositoryLayer.Context
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using FundooCommonLayer.Model;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;

    /// <summary>
    /// this class is used to create database connection
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityDbContext" />
    public class AuthenticationContext : IdentityDbContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AuthenticationContext"/> class.
        /// </summary>
        /// <param name="options">The options to be used by a <see cref="T:Microsoft.EntityFrameworkCore.DbContext" />.</param>
        public AuthenticationContext(DbContextOptions options) : base(options)
        {
        }

        /// <summary>
        /// Gets or sets the user data table.
        /// </summary>
        /// <value>
        /// The user data table.
        /// </value>
        public DbSet<ApplicationModel> UserDataTable { get; set; }

        /// <summary>
        /// Gets or sets the note.
        /// </summary>
        /// <value>
        /// The note.
        /// </value>
        public DbSet<NoteModel> Note { get; set; }

        /// <summary>
        /// Gets or sets the label.
        /// </summary>
        /// <value>
        /// The label.
        /// </value>
        public DbSet<LabelModel> Label { get; set; }

        /// <summary>
        /// Gets or sets the note label.
        /// </summary>
        /// <value>
        /// The note label.
        /// </value>
        public DbSet<NoteLabelModel> NoteLabel { get; set; }

       
    }
}
