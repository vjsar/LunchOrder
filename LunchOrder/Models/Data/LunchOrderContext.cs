namespace LunchOrder.Models.Data
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class LunchOrderContext : DbContext
    {
        public LunchOrderContext()
            : base("name=LunchOrder")
        {
        }

        public virtual DbSet<LunchLocation> LunchLocations { get; set; }
        public virtual DbSet<LunchOffice> LunchOffices { get; set; }
        public virtual DbSet<LunchProvider> LunchProviders { get; set; }
        public virtual DbSet<Lunch> Lunches { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<LunchOffice>()
                .Property(e => e.Office)
                .IsUnicode(false);

            modelBuilder.Entity<LunchOffice>()
                .Property(e => e.Country)
                .IsUnicode(false);

            modelBuilder.Entity<LunchOffice>()
                .HasMany(e => e.LunchLocations)
                .WithRequired(e => e.LunchOffice)
                .HasForeignKey(e => e.FK_OfficeId);

            modelBuilder.Entity<LunchOffice>()
                .HasMany(e => e.LunchProviders)
                .WithRequired(e => e.LunchOffice)
                .HasForeignKey(e => e.FK_OfficeId);

            modelBuilder.Entity<Lunch>()
                .Property(e => e.Login)
                .IsUnicode(false);

            modelBuilder.Entity<Lunch>()
                .Property(e => e.FullName)
                .IsUnicode(false);

            modelBuilder.Entity<Lunch>()
                .Property(e => e.Lunch1)
                .IsUnicode(false);

            modelBuilder.Entity<Lunch>()
                .Property(e => e.Exclusion)
                .IsUnicode(false);

            modelBuilder.Entity<Lunch>()
                .Property(e => e.Notes)
                .IsUnicode(false);

            modelBuilder.Entity<Lunch>()
                .Property(e => e.Platter)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Lunch>()
                .Property(e => e.Provider)
                .IsUnicode(false);
        }
    }
}
