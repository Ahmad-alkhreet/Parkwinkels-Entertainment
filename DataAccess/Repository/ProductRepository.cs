using Domain;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly DatabaseHelper _dbHelper;

        public ProductRepository(DatabaseHelper dbHelper)
        {
            _dbHelper = dbHelper;
        }

        public async Task<List<Product>> GetAllAsync()
        {
            var producten = new List<Product>();
            using (var connection = await _dbHelper.GetConnectionAsync())
            {
                var query = "SELECT * FROM Product";
                using (var command = new MySqlCommand(query, connection))
                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        producten.Add(new Product(
                            reader.GetInt32("ProductID"),
                            reader.GetString("Naam"),
                            reader.GetString("Categorie"),
                            reader.GetInt32("VoorraadAantal"),
                            reader.GetInt32("MinimaleVoorraad"),
                            reader.GetDecimal("Prijs")
                        ));
                    }
                }
            }
            return producten;
        }

        public async Task AddAsync(Product product)
        {
            using (var connection = await _dbHelper.GetConnectionAsync())
            {
                var query = @"INSERT INTO Product 
                             (Naam, Categorie, VoorraadAantal, MinimaleVoorraad, Prijs)
                             VALUES (@Naam, @Categorie, @VoorraadAantal, @MinimaleVoorraad, @Prijs)";
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Naam", product.Naam);
                    command.Parameters.AddWithValue("@Categorie", product.Categorie);
                    command.Parameters.AddWithValue("@VoorraadAantal", product.VoorraadAantal);
                    command.Parameters.AddWithValue("@MinimaleVoorraad", product.MinimaleVoorraad);
                    command.Parameters.AddWithValue("@Prijs", product.Prijs);

                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task UpdateVoorraadAsync(Product product)
        {
            using (var connection = await _dbHelper.GetConnectionAsync())
            {
                var query = "UPDATE Product SET VoorraadAantal = @Aantal WHERE ProductID = @ProductID";
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Aantal", product.VoorraadAantal);
                    command.Parameters.AddWithValue("@ProductID", product.ProductID);

                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task DeleteAsync(int productId)
        {
            using (var connection = await _dbHelper.GetConnectionAsync())
            {
                var query = "DELETE FROM Product WHERE ProductID = @ProductID";
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ProductID", productId);
                    await command.ExecuteNonQueryAsync();
                }
            }
        }
    }
}
