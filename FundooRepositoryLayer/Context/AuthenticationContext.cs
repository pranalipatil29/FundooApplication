using System;
using System.Collections.Generic;
using System.Text;
using FundooCommonLayer.Model;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FundooRepositoryLayer.Context
{
    public class AuthenticationContext:IdentityDbContext
    {
        public AuthenticationContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<ApplicationModel> UserDataTable { get; set; }

        public DbSet<NoteModel> Note { get; set; }

        public DbSet<LabelModel> Label { get; set; }

        public DbSet<NoteLabelModel> NoteLabel { get; set; }
    }
}
