using System;
using System.Data.Entity;
using System.Linq;
using DAL.Models;

namespace DAL
{
    public class AllDbContext: DbContext
    {        


        public AllDbContext():base("ParkingFine")
        {
            
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            
        }

        public DbSet<Parking> Parkings { get; set; }
        public DbSet<Fine> Fines { get; set; }
        public DbSet<Auto> Autos { get; set; }
        public DbSet<Evacuation> Evacuations { get; set; }
        public DbSet<Declaration> Declarations { get; set; }
        public DbSet<Profile> Profiles { get; set; }
    }
}
