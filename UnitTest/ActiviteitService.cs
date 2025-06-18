using Xunit;
using Moq;
using Domain;
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
        Assert.Equal("Voetbaltoernooi", result[0].Naam);
    }

    [Fact]
    public async Task AddActiviteitAsync_ThrowsException_WhenNaamIsLeeg()
    {
        var mockRepo = new Mock<IActiviteitRepository>();
        var service = new ActiviteitService(mockRepo.Object);

        await Assert.ThrowsAsync<ArgumentException>(() =>
            service.AddActiviteitAsync("", "beschrijving", "locatie", DateTime.Now, DateTime.Now.AddHours(1), 10));
    }

    [Fact]
    public async Task AddActiviteitAsync_ThrowsException_WhenStarttijdNaEindtijd()
    {
        var mockRepo = new Mock<IActiviteitRepository>();
        var service = new ActiviteitService(mockRepo.Object);

        await Assert.ThrowsAsync<ArgumentException>(() =>
            service.AddActiviteitAsync("naam", "beschrijving", "locatie", DateTime.Now.AddHours(2), DateTime.Now, 10));
    }

    [Fact]
    public async Task AddActiviteitAsync_ThrowsException_WhenMaxDeelnemersIsNul()
    {
        var mockRepo = new Mock<IActiviteitRepository>();
        var service = new ActiviteitService(mockRepo.Object);

        await Assert.ThrowsAsync<ArgumentException>(() =>
            service.AddActiviteitAsync("naam", "beschrijving", "locatie", DateTime.Now, DateTime.Now.AddHours(1), 0));
    }

    [Fact]
    public async Task AddActiviteitAsync_CallsRepositoryWithCorrectData()
    {
        // Arrange
        var mockRepo = new Mock<IActiviteitRepository>();
        var service = new ActiviteitService(mockRepo.Object);

        var start = DateTime.Now;
        var einde = start.AddHours(2);

        // Act
        await service.AddActiviteitAsync("Voetbal", "Toernooi", "Veld 1", start, einde, 15);

        // Assert
        mockRepo.Verify(r => r.AddAsync(It.Is<Activiteit>(a =>
            a.Naam == "Voetbal" &&
            a.Beschrijving == "Toernooi" &&
            a.Locatie == "Veld 1" &&
            a.Starttijd == start &&
            a.Eindtijd == einde &&
            a.MaxDeelnemers == 15
        )), Times.Once);
    }

}
