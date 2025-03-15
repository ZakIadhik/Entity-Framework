using Microsoft.EntityFrameworkCore;
using System;
using EfCore.Models;
using Microsoft.Extensions.Logging;

public class CarShowRoomContext : DbContext
{
    
    public CarShowRoomContext(DbContextOptions<CarShowRoomContext> options) : base(options) { }
    
    public DbSet<Car> Cars { get; set; }
    public DbSet<Dealer> Dealers { get; set; }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Order> Orders { get; set; }
    
    public override int SaveChanges()
    {
        var deletedCars = ChangeTracker.Entries<Car>()
            .Where(e => e.State == EntityState.Deleted)
            .ToList();

        foreach (var entry in deletedCars)
        {
            entry.State = EntityState.Modified;
            entry.Entity.IsDeleted = true; 
        }
        return base.SaveChanges();
    }
    

    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.Entity<Order>()
            .HasKey(co => new { co.CarId, co.CustomerId });
        
        modelBuilder.Entity<Car>()
            .HasIndex(c => new { c.Make, c.Model })
            .IsUnique();
        
        modelBuilder.Entity<Car>()
            .HasOne(c => c.Dealer)
            .WithMany(d => d.Cars)
            .HasForeignKey(c => c.DealerId)
            .OnDelete(DeleteBehavior.Cascade);
        
        modelBuilder.Entity<Order>()
            .HasKey(o => o.Id);
        
        modelBuilder.Entity<Order>()
            .Property(o => o.Id)
            .ValueGeneratedOnAdd();
        
        modelBuilder.Entity<Order>()
            .HasOne(co => co.Car)
            .WithMany(c => c.Orders) 
            .HasForeignKey(co => co.CarId);

        modelBuilder.Entity<Order>()
            .HasOne(co => co.Customer)
            .WithMany(c => c.Orders)  
            .HasForeignKey(co => co.CustomerId);

        modelBuilder.Entity<Dealer>()
            .HasIndex(d => d.Name)
            .IsUnique();
    }
}