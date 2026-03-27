using Microsoft.EntityFrameworkCore;
using StudyTool.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace StudyTool.Data
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        public DbSet<Card> Cards => Set<Card>();
        public DbSet<Group> Groups => Set<Group>();
        public DbSet<TallyEvent> TallyEvents => Set<TallyEvent>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Group>(e =>
            {
                e.HasKey(g => g.Id);
                e.Property(g => g.Name).HasMaxLength(100).IsRequired();
                e.Property(g => g.CreatedBy).HasMaxLength(50).IsRequired();
            });

            modelBuilder.Entity<Card>(e =>
            {
                e.HasKey(c => c.Id);
                e.Property(c => c.Title).HasMaxLength(500).IsRequired();
                e.Property(c => c.CreatedBy).HasMaxLength(50).IsRequired();
                e.HasOne(c => c.Group)
                 .WithMany(g => g.Cards)
                 .HasForeignKey(c => c.GroupId)
                 .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<TallyEvent>(e =>
            {
                e.HasKey(t => t.Id);
                e.Property(t => t.UserId).HasMaxLength(50).IsRequired();
                e.HasOne(t => t.Card)
                 .WithMany(c => c.TallyEvents)
                 .HasForeignKey(t => t.CardId)
                 .OnDelete(DeleteBehavior.Cascade);
            });
        }
    }
}
