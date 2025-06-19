using Xunit;
using Moq;
using Domain;
using Service;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;


public class ActiviteitServiceTests
{
    // 1. Test: ophalen van een enkele activiteit
    [Fact]
    public async Task GetAllActiviteitenAsync_ReturnsListOfActiviteiten()
    {
        var activiteiten = new List<Activiteit>
        {
            new Activiteit(1, "Voetbaltoernooi", "Toernooi op veld 1", "Sportveld",
                DateTime.Today.AddHours(10), DateTime.Today.AddHours(12), 20)
        };

        var mockRepo = new Mock<IActiviteitRepository>();
        mockRepo.Setup(r => r.GetAllAsync()).ReturnsAsync(activiteiten);

        var service = new ActiviteitService(mockRepo.Object);

        var result = await service.GetAllActiviteitenAsync();

        Assert.Single(result);
        Assert.Equal("Voetbaltoernooi", result[0].Naam);
    }

    // 2. Test: toevoegen van een geldige activiteit roept repository aan
    [Fact]
    public async Task AddActiviteitAsync_CallsRepositoryWithCorrectData()
    {
        var mockRepo = new Mock<IActiviteitRepository>();
        var service = new ActiviteitService(mockRepo.Object);

        var start = DateTime.Now;
        var einde = start.AddHours(2);

        var activiteit = new Activiteit(0, "Voetbal", "Toernooi", "Veld 1", start, einde, 15);

        await service.AddActiviteitAsync(activiteit);

        mockRepo.Verify(r => r.AddAsync(It.Is<Activiteit>(a =>
            a.Naam == "Voetbal" &&
            a.Beschrijving == "Toernooi" &&
            a.Locatie == "Veld 1" &&
            a.Starttijd == start &&
            a.Eindtijd == einde &&
            a.MaxDeelnemers == 15
        )), Times.Once);
    }

    // 3. Test: toevoegen null activiteit gooit ArgumentNullException
    [Fact]
    public async Task AddActiviteitAsync_ThrowsArgumentNullException_WhenActiviteitIsNull()
    {
        var mockRepo = new Mock<IActiviteitRepository>();
        var service = new ActiviteitService(mockRepo.Object);

        await Assert.ThrowsAsync<ArgumentNullException>(() => service.AddActiviteitAsync(null));
    }

    // 4. Test: ophalen geeft lege lijst terug
    [Fact]
    public async Task GetAllActiviteitenAsync_ReturnsEmptyList_WhenNoActivities()
    {
        var mockRepo = new Mock<IActiviteitRepository>();
        mockRepo.Setup(r => r.GetAllAsync()).ReturnsAsync(new List<Activiteit>());

        var service = new ActiviteitService(mockRepo.Object);

        var result = await service.GetAllActiviteitenAsync();

        Assert.Empty(result);
    }

    // 5. Test: ophalen geeft meerdere activiteiten terug
    [Fact]
    public async Task GetAllActiviteitenAsync_ReturnsMultipleActivities()
    {
        var activiteiten = new List<Activiteit>
        {
            new Activiteit(1, "Activiteit 1", "Beschrijving 1", "Locatie 1", DateTime.Now, DateTime.Now.AddHours(1), 10),
            new Activiteit(2, "Activiteit 2", "Beschrijving 2", "Locatie 2", DateTime.Now, DateTime.Now.AddHours(2), 15)
        };

        var mockRepo = new Mock<IActiviteitRepository>();
        mockRepo.Setup(r => r.GetAllAsync()).ReturnsAsync(activiteiten);

        var service = new ActiviteitService(mockRepo.Object);

        var result = await service.GetAllActiviteitenAsync();

        Assert.Equal(2, result.Count);
    }

    // 6. Test: constructor gooit exception bij lege naam + juiste message
    [Fact]
    public void Constructor_ThrowsException_WhenNaamIsLeeg()
    {
        var ex = Assert.Throws<ArgumentException>(() =>
            new Activiteit(0, "", "beschrijving", "locatie", DateTime.Now, DateTime.Now.AddHours(1), 10));
        Assert.Equal("Naam mag niet leeg zijn.", ex.Message);
    }

    // 7. Test: constructor gooit exception bij starttijd >= eindtijd + juiste message
    [Fact]
    public void Constructor_ThrowsException_WhenStarttijdNaEindtijd()
    {
        var ex = Assert.Throws<ArgumentException>(() =>
            new Activiteit(0, "naam", "beschrijving", "locatie", DateTime.Now.AddHours(2), DateTime.Now, 10));
        Assert.Equal("Starttijd moet voor eindtijd liggen.", ex.Message);
    }

    // 8. Test: constructor gooit exception bij maxDeelnemers <= 0 + juiste message
    [Fact]
    public void Constructor_ThrowsException_WhenMaxDeelnemersIsNul()
    {
        var ex = Assert.Throws<ArgumentException>(() =>
            new Activiteit(0, "naam", "beschrijving", "locatie", DateTime.Now, DateTime.Now.AddHours(1), 0));
        Assert.Equal("Maximaal aantal deelnemers moet groter zijn dan nul.", ex.Message);
    }

    // 9. Test: Activiteit object met valide input wordt goed gecreëerd
    [Fact]
    public void Constructor_CreatesObject_WhenValidInput()
    {
        var start = DateTime.Now;
        var einde = start.AddHours(1);
        var activiteit = new Activiteit(1, "Naam", "Beschrijving", "Locatie", start, einde, 10);

        Assert.Equal(1, activiteit.ActiviteitID);
        Assert.Equal("Naam", activiteit.Naam);
        Assert.Equal("Beschrijving", activiteit.Beschrijving);
        Assert.Equal("Locatie", activiteit.Locatie);
        Assert.Equal(start, activiteit.Starttijd);
        Assert.Equal(einde, activiteit.Eindtijd);
        Assert.Equal(10, activiteit.MaxDeelnemers);
    }

    // 10. Test: repository gooit exception, service geeft deze door
    [Fact]
    public async Task AddActiviteitAsync_PropagatesExceptionFromRepository()
    {
        var mockRepo = new Mock<IActiviteitRepository>();
        mockRepo.Setup(r => r.AddAsync(It.IsAny<Activiteit>())).ThrowsAsync(new InvalidOperationException());

        var service = new ActiviteitService(mockRepo.Object);

        var activiteit = new Activiteit(0, "Naam", "Desc", "Loc", DateTime.Now, DateTime.Now.AddHours(1), 5);

        await Assert.ThrowsAsync<InvalidOperationException>(() => service.AddActiviteitAsync(activiteit));
    }
}
