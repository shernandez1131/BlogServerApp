using BlogApp.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace BlogApp.Data
{
    public class BlogDbContext : DbContext
    {
        public BlogDbContext(DbContextOptions<BlogDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<BlogEntry> BlogEntries { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<BlogEntryCategory> BlogEntryCategories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BlogEntryCategory>()
                .HasKey(bc => new { bc.BlogEntryId, bc.CategoryId });

            modelBuilder.Entity<BlogEntryCategory>()
                .HasOne(bc => bc.BlogEntry)
                .WithMany(b => b.BlogEntryCategories)
                .HasForeignKey(bc => bc.BlogEntryId);

            modelBuilder.Entity<BlogEntryCategory>()
                .HasOne(bc => bc.Category)
                .WithMany(c => c.BlogEntryCategories)
                .HasForeignKey(bc => bc.CategoryId);
        }
    }
}
