using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace MeterReaderCMS.Models.Entities
{
    public class MeterReaderDB : DbContext
    {
        public MeterReaderDB():base("MeterReaderDB")
        {

        }

        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<MeterReader> MetersReaders { get; set; }
        public DbSet<Notebook> Notesbooks { get; set; }
        public DbSet<Track> Tracks { get; set; }
    }
}