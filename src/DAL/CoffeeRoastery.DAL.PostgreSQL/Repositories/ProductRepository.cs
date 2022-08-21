using System.Threading.Tasks;
using CoffeeRoastery.DAL.Domain.Models;
using CoffeeRoastery.DAL.Interface.Repositories;
using CoffeeRoastery.DAL.PostgreSQL.Context;
using Microsoft.EntityFrameworkCore;

namespace CoffeeRoastery.DAL.PostgreSQL.Repositories;

public class ProductRepository : SQLRepository<Product>, IProductRepository
{
    public ProductRepository(CoffeeRoasteryContext dbContext) : base(dbContext)
    {
    }
    
    public Task<bool> ExistsByName(string name)
    {
        return dbSet.AnyAsync(tenant => tenant.Name == name);
    }
}