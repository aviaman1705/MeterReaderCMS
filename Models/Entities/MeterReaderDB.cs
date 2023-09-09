using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace MeterReaderCMS.Models.Entities
{
    public class MeterReaderDB : DbContext
    {
        public MeterReaderDB() : base("MeterReaderDB")
        {

        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Track> Tracks { get; set; }
        public DbSet<Notebook> Notebooks { get; set; }        
        public DbSet<Street> Streets { get; set; }
    }
}