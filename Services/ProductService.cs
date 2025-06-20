﻿using Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service
{
    public class ProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<List<Product>> GetAllProductsAsync()
        {
            return await _productRepository.GetAllAsync();
        }

        public async Task AddProductAsync(string naam, string categorie, int voorraad, int minimum, decimal prijs)
        {

            if (string.IsNullOrWhiteSpace(naam))
                throw new ArgumentException("Naam mag niet leeg zijn.");

            if (string.IsNullOrWhiteSpace(categorie))
                throw new ArgumentException("Categorie mag niet leeg zijn.");

            if (voorraad < 0)
                throw new ArgumentException("Voorraad mag niet negatief zijn.");

            if (minimum < 0)
                throw new ArgumentException("Minimale voorraad mag niet negatief zijn.");

            if (prijs < 0)
                throw new ArgumentException("Prijs mag niet negatief zijn.");

            var product = new Product(0, naam, categorie, voorraad, minimum, prijs);
            await _productRepository.AddAsync(product);
        }


        public async Task UpdateVoorraadAsync(Product product, int nieuwAantal)
        {
            if (nieuwAantal < 0)
                throw new ArgumentException("Voorraad mag niet negatief zijn.");

            product.UpdateVoorraad(nieuwAantal); 
            await _productRepository.UpdateVoorraadAsync(product); 
        }




        public async Task DeleteProductAsync(int productId)
        {
            await _productRepository.DeleteAsync(productId);
        }
    }
}
