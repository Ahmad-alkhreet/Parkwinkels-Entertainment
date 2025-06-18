using Xunit;
using Moq;
using Service;
using Domain;
using System.Threading.Tasks;
using System.Collections.Generic;

public class NieuwsberichtServiceTests
{
    [Fact]
    public async Task AddNieuwsberichtAsync_CreatesNieuwsberichtCorrect()
    {
        // Arrange
        var mockRepo = new Mock<INieuwsberichtRepository>();
        var service = new NieuwsberichtService(mockRepo.Object);

        // Act
        await service.AddNieuwsberichtAsync("Test Titel", "Test inhoud");

        // Assert
        mockRepo.Verify(r => r.AddAsync(It.Is<Nieuwsbericht>(
           n => n != null &&
                n.Titel == "Test Titel" &&
                n.Inhoud == "Test inhoud"
       )), Times.Once);

    }

    [Fact]
    public async Task GetAllNieuwsAsync_ReturnsNieuwsberichten()
    {
        // Arrange
        var dummyList = new List<Nieuwsbericht>
        {
            new Nieuwsbericht(1, "Titel 1", "Inhoud 1", System.DateTime.Now)
        };

        var mockRepo = new Mock<INieuwsberichtRepository>();
        mockRepo.Setup(r => r.GetAllAsync()).ReturnsAsync(dummyList);

        var service = new NieuwsberichtService(mockRepo.Object);

        // Act
        var result = await service.GetAllNieuwsAsync();

        // Assert
        Assert.Single(result);
        Assert.Equal("Titel 1", result[0].Titel);
    }

    [Fact]
    public async Task AddNieuwsberichtAsync_ThrowsException_WhenTitelIsLeeg()
    {
        // Arrange
        var mockRepo = new Mock<INieuwsberichtRepository>();
        var service = new NieuwsberichtService(mockRepo.Object);

        // Act & Assert
        var ex = await Assert.ThrowsAsync<ArgumentException>(() =>
            service.AddNieuwsberichtAsync("", "Geldige inhoud")
        );
        Assert.Equal("Titel mag niet leeg zijn.", ex.Message);

        mockRepo.Verify(r => r.AddAsync(It.IsAny<Nieuwsbericht>()), Times.Never);
    }

    [Fact]
    public async Task AddNieuwsberichtAsync_ThrowsException_WhenInhoudIsLeeg()
    {
        // Arrange
        var mockRepo = new Mock<INieuwsberichtRepository>();
        var service = new NieuwsberichtService(mockRepo.Object);

        // Act & Assert
        var ex = await Assert.ThrowsAsync<ArgumentException>(() =>
            service.AddNieuwsberichtAsync("Geldige titel", "  ")
        );
        Assert.Equal("Inhoud mag niet leeg zijn.", ex.Message);

        mockRepo.Verify(r => r.AddAsync(It.IsAny<Nieuwsbericht>()), Times.Never);
    }

}
