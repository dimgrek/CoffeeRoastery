using CoffeeRoastery.DAL.Domain.Models;
using CoffeeRoastery.DAL.Interface.Repositories;
using CoffeeRoastery.DAL.PostgreSQL.Context;

namespace CoffeeRoastery.DAL.PostgreSQL.Repositories;

public class ProductRepository : SQLRepository<Product>, IProductRepository
{
    public ProductRepository(CoffeeRoasteryContext dbContext) : base(dbContext)
    {
    }
}