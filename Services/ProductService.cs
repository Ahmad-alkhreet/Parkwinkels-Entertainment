using DataAccess.Repositories;
using Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service
{
    public class ProductService
    {
        private readonly ProductRepository _productRepository;

        public ProductService(ProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<List<Product>> GetAllProductsAsync()
        {
            return await _productRepository.GetAllAsync();
        }

        public async Task AddProductAsync(string naam, string categorie, int voorraad, int minimum, decimal prijs)
        {
            var product = new Product(0, naam, categorie, voorraad, minimum, prijs);
            await _productRepository.AddAsync(product);
        }

        public async Task UpdateVoorraadAsync(int productId, int nieuwAantal)
        {
            await _productRepository.UpdateVoorraadAsync(productId, nieuwAantal);
        }

        public async Task DeleteProductAsync(int productId)
        {
            await _productRepository.DeleteAsync(productId);
        }
    }
}
