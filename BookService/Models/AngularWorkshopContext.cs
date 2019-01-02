using System;
using Microsoft.EntityFrameworkCore;

namespace BookService.Models
{
    public partial class AngularWorkshopContext : DbContext
    {
        public virtual DbSet<Book> Book { get; set; }

        public AngularWorkshopContext(DbContextOptions<AngularWorkshopContext> options)
        : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedNever();

                entity.Property(e => e.Author)
                    .IsRequired()
                    .HasColumnName("author")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.IsCheckedOut).HasColumnName("isCheckedOut");

                entity.Property(e => e.Rating).HasColumnName("rating");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasColumnName("title")
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });
        }
    }
}
