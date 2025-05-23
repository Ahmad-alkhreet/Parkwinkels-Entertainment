using Xunit;
using Moq;
using Domain;
using DataAccess.Repositories;
using Service;
using System.Collections.Generic;
using System.Threading.Tasks;

public class ActiviteitServiceTests
{
    [Fact]
    public async Task GetAllActiviteitenAsync_ReturnsListOfActiviteiten()
    {
        // Arrange
        var activiteiten = new List<Activiteit>
        {
            new Activiteit(1, "Voetbaltoernooi", "Toernooi op veld 1", "Sportveld", DateTime.Today.AddHours(10), DateTime.Today.AddHours(12), 20)
        };

        var mockRepo = new Mock<IActiviteitRepository>();
        mockRepo.Setup(r => r.GetAllAsync()).ReturnsAsync(activiteiten);

        var service = new ActiviteitService(mockRepo.Object);

        // Act
        var result = await service.GetAllActiviteitenAsync();

        // Assert
        Assert.Single(result);
        Assert.Equal("Voetbaltoernooi", result[0].GetNaam());
    }
}
