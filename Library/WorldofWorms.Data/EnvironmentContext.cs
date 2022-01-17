using Microsoft.EntityFrameworkCore;
using System;

namespace WorldofWorms.Data
{
    public class EnvironmentContext : DbContext
    {
        public EnvironmentContext() : base() { }
        public EnvironmentContext(DbContextOptions<EnvironmentContext> options)
           : base(options)
        {
            //Database.EnsureCreated();
        }

        public DbSet<Behavior> Behaviours { get; set; }

        public DbSet<BehaviorInfo> BehaviourSteps { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Behavior>().HasKey(e => e.Id);
            modelBuilder.Entity<Behavior>().Property(e => e.Name).IsRequired(true);
            modelBuilder.Entity<Behavior>().HasIndex(e => e.Name).IsUnique(true);
            modelBuilder.Entity<Behavior>().HasMany(e => e.Steps).WithOne(s => s.Behavior).HasForeignKey(s => s.BehaviorId).IsRequired(true).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Behavior>().Property(u => u.Id).ValueGeneratedNever();


            modelBuilder.Entity<BehaviorInfo>().HasKey(e => new { e.BehaviorId, e.Order });
            modelBuilder.Entity<BehaviorInfo>().Property(e => e.X).IsRequired(true);
            modelBuilder.Entity<BehaviorInfo>().Property(e => e.Y).IsRequired(true);
        }
    }
}
