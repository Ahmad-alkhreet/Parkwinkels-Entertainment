using Xunit;
using Moq;
using Service;
using Domain;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace UnitTest
{
    public class ProductServiceTests
    {
        // Test of GetAllProductsAsync een lijst met producten teruggeeft
        [Fact]
        public async Task GetAllProductsAsync_ReturnsListOfProducts()
        {
            var producten = new List<Product>
        {
            new Product(1, "Chips", "Snack", 50, 5, 1.99m)
        };

            var mockRepo = new Mock<IProductRepository>();
            mockRepo.Setup(r => r.GetAllAsync()).ReturnsAsync(producten);

            var service = new ProductService(mockRepo.Object);

            var result = await service.GetAllProductsAsync();

            Assert.Single(result);
            Assert.Equal("Chips", result[0].Naam);
        }

        // Test of AddProductAsync de repository correct aanroept met de juiste productgegevens
        [Fact]
        public async Task AddProductAsync_CallsRepositoryWithCorrectProduct()
        {
            var mockRepo = new Mock<IProductRepository>();
            var service = new ProductService(mockRepo.Object);

            await service.AddProductAsync("Cola", "Drinken", 100, 10, 1.50m);

            mockRepo.Verify(r => r.AddAsync(It.Is<Product>(p =>
                p.Naam == "Cola" &&
                p.Categorie == "Drinken" &&
                p.VoorraadAantal == 100 &&
                p.MinimaleVoorraad == 10 &&
                p.Prijs == 1.50m
            )), Times.Once);
        }

        // Test of AddProductAsync een exception gooit bij ongeldige input (verschillende cases)
        [Theory]
        [InlineData("", "Drinken", 10, 5, 1.99, "Naam mag niet leeg zijn.")]
        [InlineData("Cola", "", 10, 5, 1.99, "Categorie mag niet leeg zijn.")]
        [InlineData("Cola", "Drinken", -1, 5, 1.99, "Voorraad mag niet negatief zijn.")]
        [InlineData("Cola", "Drinken", 10, -2, 1.99, "Minimale voorraad mag niet negatief zijn.")]
        [InlineData("Cola", "Drinken", 10, 5, -0.50, "Prijs mag niet negatief zijn.")]
        public async Task AddProductAsync_ThrowsException_OnInvalidInput(
            string naam, string categorie, int voorraad, int minimum, decimal prijs, string expectedMessage)
        {
            var mockRepo = new Mock<IProductRepository>();
            var service = new ProductService(mockRepo.Object);

            var ex = await Assert.ThrowsAsync<ArgumentException>(() =>
                service.AddProductAsync(naam, categorie, voorraad, minimum, prijs)
            );

            Assert.Equal(expectedMessage, ex.Message);
            // Zorg dat repository niet wordt aangeroepen bij foutieve input
            mockRepo.Verify(r => r.AddAsync(It.IsAny<Product>()), Times.Never);
        }

        // Test of UpdateVoorraadAsync het product correct bijwerkt en repository aanroept
        [Fact]
        public async Task UpdateVoorraadAsync_UpdatesProductCorrectly()
        {
            var product = new Product(1, "Cola", "Drinken", 50, 10, 1.99m);
            var mockRepo = new Mock<IProductRepository>();
            var service = new ProductService(mockRepo.Object);

            await service.UpdateVoorraadAsync(product, 30);

            Assert.Equal(30, product.VoorraadAantal);
            mockRepo.Verify(r => r.UpdateVoorraadAsync(product), Times.Once);
        }

        // Test of UpdateVoorraadAsync een exception gooit wanneer voorraad negatief is
        [Fact]
        public async Task UpdateVoorraadAsync_ThrowsException_WhenAantalIsNegatief()
        {
            var product = new Product(1, "Cola", "Drinken", 50, 10, 1.99m);
            var mockRepo = new Mock<IProductRepository>();
            var service = new ProductService(mockRepo.Object);

            await Assert.ThrowsAsync<ArgumentException>(() =>
                service.UpdateVoorraadAsync(product, -5)
            );

            // Zorg dat repository niet wordt aangeroepen bij foutieve input
            mockRepo.Verify(r => r.UpdateVoorraadAsync(It.IsAny<Product>()), Times.Never);
        }

        // Test of DeleteProductAsync de repository correct aanroept om een product te verwijderen
        [Fact]
        public async Task DeleteProductAsync_CallsRepositoryCorrectly()
        {
            var mockRepo = new Mock<IProductRepository>();
            var service = new ProductService(mockRepo.Object);

            await service.DeleteProductAsync(2);

            mockRepo.Verify(r => r.DeleteAsync(2), Times.Once);
        }
    }
}
