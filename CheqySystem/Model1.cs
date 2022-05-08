namespace CheqySystem
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class Model1 : DbContext
    {
        public Model1()
            : base("name=Model1")
        {
        }

        public virtual DbSet<DipsatchInfo> DipsatchInfoes { get; set; }
        public virtual DbSet<SectorsInfo> SectorsInfoes { get; set; }
        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            modelBuilder.Entity<SectorsInfo>()
                .HasMany(e => e.DipsatchInfoes)
                .WithOptional(e => e.SectorsInfo)
                .WillCascadeOnDelete();
        }
    }
}
