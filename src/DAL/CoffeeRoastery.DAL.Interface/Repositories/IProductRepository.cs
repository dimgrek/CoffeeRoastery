using System.Threading.Tasks;
using CoffeeRoastery.DAL.Domain.Models;

namespace CoffeeRoastery.DAL.Interface.Repositories;

public interface IProductRepository : IRepository<Product>
{
    Task<bool> ExistsByName(string name);
}