using Microsoft.EntityFrameworkCore;
using fleetsystem.entities;

namespace fleetsystem.config;

public class DriverDbContext : DbContext
{
    public DriverDbContext(DbContextOptions<DriverDbContext> options) : base(options) { }

    public DbSet<Driver> Drivers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Driver>().HasKey(d => d.Id);
        base.OnModelCreating(modelBuilder);
    }
}