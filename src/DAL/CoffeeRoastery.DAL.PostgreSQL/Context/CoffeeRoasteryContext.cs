using CoffeeRoastery.DAL.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace CoffeeRoastery.DAL.PostgreSQL.Context;

public class CoffeeRoasteryContext : DbContext
{
    public DbSet<Product> Products { get; set; }

    public CoffeeRoasteryContext(DbContextOptions<CoffeeRoasteryContext> options) : base(options)
    {
        Database.EnsureCreated();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        #region Data

        

        #endregion
    }
}