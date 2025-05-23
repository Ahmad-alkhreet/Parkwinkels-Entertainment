using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain
{
    public interface IProductRepository
    {
        Task<List<Product>> GetAllAsync();
        Task AddAsync(Product product);
        Task UpdateVoorraadAsync(int productId, int nieuwAantal);
        Task DeleteAsync(int productId);
    }
}
