using Xunit;
using Moq;
using Service;
using Domain;
using System.Collections.Generic;
using System.Threading.Tasks;


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
        Assert.Equal("Chips", result[0].Naam);
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
            p.Naam == "Cola" &&
            p.Categorie == "Drinken" &&
            p.VoorraadAantal == 100 &&
            p.MinimaleVoorraad == 10 &&
            p.Prijs == 1.50m
        )), Times.Once);
    }
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
        mockRepo.Verify(r => r.AddAsync(It.IsAny<Product>()), Times.Never);
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

    [Fact]
    public async Task UpdateVoorraadAsync_ThrowsException_WhenAantalIsNegatief()
    {
        // Arrange
        var mockRepo = new Mock<IProductRepository>();
        var service = new ProductService(mockRepo.Object);

        // Act & Assert
        var exception = await Assert.ThrowsAsync<ArgumentException>(() =>
            service.UpdateVoorraadAsync(1, -10)
        );

        Assert.Equal("Voorraad mag niet negatief zijn.", exception.Message);
        mockRepo.Verify(r => r.UpdateVoorraadAsync(It.IsAny<int>(), It.IsAny<int>()), Times.Never);
    }

    [Fact]
    public async Task UpdateVoorraadAsync_DoesNotThrow_WhenAantalIsZero()
    {
        // Arrange
        var mockRepo = new Mock<IProductRepository>();
        var service = new ProductService(mockRepo.Object);

        // Act
        await service.UpdateVoorraadAsync(1, 0);

        // Assert
        mockRepo.Verify(r => r.UpdateVoorraadAsync(1, 0), Times.Once);
    }

    [Fact]
    public async Task UpdateVoorraadAsync_DoesNotThrow_WhenAantalIsPositief()
    {
        // Arrange
        var mockRepo = new Mock<IProductRepository>();
        var service = new ProductService(mockRepo.Object);

        // Act
        await service.UpdateVoorraadAsync(1, 25);

        // Assert
        mockRepo.Verify(r => r.UpdateVoorraadAsync(1, 25), Times.Once);
    }
}
