namespace LunchOrder.Models.EF
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class LunchOrderDBContext : DbContext
    {
        public LunchOrderDBContext()
            : base("name=LunchOrderDBContext")
        {
        }

        public virtual DbSet<LunchLocation> LunchLocations { get; set; }
        public virtual DbSet<LunchOffice> LunchOffices { get; set; }
        public virtual DbSet<LunchOrder> LunchOrders { get; set; }
        public virtual DbSet<LunchProfile> LunchProfiles { get; set; }
        public virtual DbSet<LunchProvider> LunchProviders { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<LunchLocation>()
                .HasMany(e => e.LunchProfiles)
                .WithRequired(e => e.LunchLocation)
                .HasForeignKey(e => e.FK_LocationId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<LunchOffice>()
                .Property(e => e.Office)
                .IsUnicode(false);

            modelBuilder.Entity<LunchOffice>()
                .Property(e => e.Country)
                .IsUnicode(false);

            modelBuilder.Entity<LunchOffice>()
                .HasMany(e => e.LunchLocations)
                .WithRequired(e => e.LunchOffice)
                .HasForeignKey(e => e.FK_OfficeId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<LunchOffice>()
                .HasMany(e => e.LunchProviders)
                .WithRequired(e => e.LunchOffice)
                .HasForeignKey(e => e.FK_OfficeId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<LunchOffice>()
                .HasMany(e => e.LunchProfiles)
                .WithRequired(e => e.LunchOffice)
                .HasForeignKey(e => e.FK_OfficeId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<LunchOrder>()
                .Property(e => e.Lunch)
                .IsUnicode(false);

            modelBuilder.Entity<LunchProfile>()
                .Property(e => e.Login)
                .IsUnicode(false);

            modelBuilder.Entity<LunchProfile>()
                .Property(e => e.FirstName)
                .IsUnicode(false);

            modelBuilder.Entity<LunchProfile>()
                .Property(e => e.LastName)
                .IsUnicode(false);

            modelBuilder.Entity<LunchProfile>()
                .Property(e => e.Fav1)
                .IsUnicode(false);

            modelBuilder.Entity<LunchProfile>()
                .Property(e => e.Fav2)
                .IsUnicode(false);

            modelBuilder.Entity<LunchProfile>()
                .Property(e => e.Fav3)
                .IsUnicode(false);

            modelBuilder.Entity<LunchProfile>()
                .HasMany(e => e.LunchOrders)
                .WithRequired(e => e.LunchProfile)
                .HasForeignKey(e => e.FK_ProfileId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<LunchProvider>()
                .HasMany(e => e.LunchProfiles)
                .WithRequired(e => e.LunchProvider)
                .HasForeignKey(e => e.FK_ProviderId)
                .WillCascadeOnDelete(false);
        }

    }
}
