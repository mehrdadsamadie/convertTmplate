namespace convertTmplate.Models.Entity
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class ConvertEntity : DbContext
    {
        public ConvertEntity()
            : base("name=ConvertEntity")
        {
        }

        public virtual DbSet<File1> File1 { get; set; }
        public virtual DbSet<File2> File2 { get; set; }
        public virtual DbSet<File3> File3 { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
