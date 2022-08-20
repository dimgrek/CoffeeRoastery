using Microsoft.EntityFrameworkCore;

namespace CoffeeRoastery.DAL.PostgreSQL.Context;

public class CoffeeRoasteryContext : DbContext
{

    public CoffeeRoasteryContext(DbContextOptions<CoffeeRoasteryContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
    }
}