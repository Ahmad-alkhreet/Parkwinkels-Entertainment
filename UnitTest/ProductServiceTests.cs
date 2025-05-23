using Xunit;
using Moq;
using Service;
using Domain;
using System.Collections.Generic;
using System.Threading.Tasks;
using DataAccess.Repositories;

public class ProductServiceTests
{
    [Fact]
    public async Task GetAllProductsAsync_ReturnsListOfProducts()
    {
        // Arrange
        var producten = new List<Product>
        {
            new Product(1, "Chips", "Snack", 50, 5, 1.99m)
        };

        var mockRepo = new Mock<IProductRepository>();
        mockRepo.Setup(r => r.GetAllAsync()).ReturnsAsync(producten);

        var service = new ProductService(mockRepo.Object);

        // Act
        var result = await service.GetAllProductsAsync();

        // Assert
        Assert.Single(result);
        Assert.Equal("Chips", result[0].GetNaam());
    }

    [Fact]
    public async Task AddProductAsync_CallsRepositoryWithCorrectProduct()
    {
        // Arrange
        var mockRepo = new Mock<IProductRepository>();
        var service = new ProductService(mockRepo.Object);

        // Act
        await service.AddProductAsync("Cola", "Drinken", 100, 10, 1.50m);

        // Assert
        mockRepo.Verify(r => r.AddAsync(It.Is<Product>(p =>
            p != null &&
            p.GetNaam() == "Cola" &&
            p.GetCategorie() == "Drinken" &&
            p.GetVoorraadAantal() == 100 &&
            p.GetMinimaleVoorraad() == 10 &&
            p.GetPrijs() == 1.50m
        )), Times.Once);
    }

    [Fact]
    public async Task UpdateVoorraadAsync_CallsRepositoryCorrectly()
    {
        // Arrange
        var mockRepo = new Mock<IProductRepository>();
        var service = new ProductService(mockRepo.Object);

        // Act
        await service.UpdateVoorraadAsync(1, 75);

        // Assert
        mockRepo.Verify(r => r.UpdateVoorraadAsync(1, 75), Times.Once);
    }

    [Fact]
    public async Task DeleteProductAsync_CallsRepositoryCorrectly()
    {
        // Arrange
        var mockRepo = new Mock<IProductRepository>();
        var service = new ProductService(mockRepo.Object);

        // Act
        await service.DeleteProductAsync(2);

        // Assert
        mockRepo.Verify(r => r.DeleteAsync(2), Times.Once);
    }
}
