using Xunit;
using Moq;
using Domain;
using Service;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public class NieuwsberichtTests
{
    // --- Constructor validatie tests ---

    // Test of de constructor een exception gooit bij lege titel
    [Fact]
    public void Constructor_ThrowsException_WhenTitelIsLeeg()
    {
        var ex = Assert.Throws<ArgumentException>(() => new Nieuwsbericht(0, "", "inhoud", DateTime.Now));
        Assert.Contains("Titel mag niet leeg zijn.", ex.Message);
    }

    // Test of de constructor een exception gooit bij lege of whitespace inhoud
    [Fact]
    public void Constructor_ThrowsException_WhenInhoudIsLeeg()
    {
        var ex = Assert.Throws<ArgumentException>(() => new Nieuwsbericht(0, "titel", "   ", DateTime.Now));
        Assert.Contains("Inhoud mag niet leeg zijn.", ex.Message);
    }

    // Test of de constructor een exception gooit als publicatiedatum in de toekomst ligt
    [Fact]
    public void Constructor_ThrowsException_WhenPublicatiedatumInDeToekomst()
    {
        var toekomst = DateTime.Now.AddDays(1);
        var ex = Assert.Throws<ArgumentException>(() => new Nieuwsbericht(0, "titel", "inhoud", toekomst));
        Assert.Contains("Publicatiedatum mag niet in de toekomst liggen.", ex.Message);
    }

    // Test of de constructor een object correct aanmaakt bij geldige input
    [Fact]
    public void Constructor_CreatesObject_WhenValidInput()
    {
        var nu = DateTime.Now;
        var nieuws = new Nieuwsbericht(0, "titel", "inhoud", nu);

        Assert.Equal("titel", nieuws.Titel);
        Assert.Equal("inhoud", nieuws.Inhoud);
        Assert.Equal(nu, nieuws.Publicatiedatum);
    }

    // Test of alle properties van het object correct zijn ingesteld
    [Fact]
    public void Nieuwsbericht_PropertiesAreSetCorrectly()
    {
        var nu = DateTime.Now;
        var nieuws = new Nieuwsbericht(1, "Titel", "Inhoud", nu);

        Assert.Equal(1, nieuws.NieuwsID);
        Assert.Equal("Titel", nieuws.Titel);
        Assert.Equal("Inhoud", nieuws.Inhoud);
        Assert.Equal(nu, nieuws.Publicatiedatum);
    }

    // --- Service tests ---

    // Test of AddNieuwsberichtAsync de repository aanroept met een geldig nieuwsbericht
    [Fact]
    public async Task AddNieuwsberichtAsync_CallsRepositoryWithValidNieuwsbericht()
    {
        var mockRepo = new Mock<INieuwsberichtRepository>();
        var service = new NieuwsberichtService(mockRepo.Object);

        var nieuws = new Nieuwsbericht(0, "Goede titel", "Goede inhoud", DateTime.Now);

        await service.AddNieuwsberichtAsync(nieuws);

        mockRepo.Verify(r => r.AddAsync(It.Is<Nieuwsbericht>(
            n => n.Titel == "Goede titel" && n.Inhoud == "Goede inhoud")), Times.Once);
    }

    // Test of GetAllNieuwsAsync een lijst met nieuwsberichten teruggeeft
    [Fact]
    public async Task GetAllNieuwsAsync_ReturnsNieuwsberichten()
    {
        var dummyList = new List<Nieuwsbericht>
        {
            new Nieuwsbericht(1, "Titel 1", "Inhoud 1", DateTime.Now)
        };

        var mockRepo = new Mock<INieuwsberichtRepository>();
        mockRepo.Setup(r => r.GetAllAsync()).ReturnsAsync(dummyList);

        var service = new NieuwsberichtService(mockRepo.Object);

        var result = await service.GetAllNieuwsAsync();

        Assert.Single(result);
        Assert.Equal("Titel 1", result[0].Titel);
    }

    // Test of GetAllNieuwsAsync een lege lijst teruggeeft als er geen nieuwsberichten zijn
    [Fact]
    public async Task GetAllNieuwsAsync_ReturnsEmptyList_WhenNoNieuwsberichten()
    {
        var mockRepo = new Mock<INieuwsberichtRepository>();
        mockRepo.Setup(r => r.GetAllAsync()).ReturnsAsync(new List<Nieuwsbericht>());

        var service = new NieuwsberichtService(mockRepo.Object);

        var result = await service.GetAllNieuwsAsync();

        Assert.Empty(result);
    }

    // Test of AddNieuwsberichtAsync een ArgumentNullException gooit als null wordt doorgegeven
    [Fact]
    public async Task AddNieuwsberichtAsync_ThrowsArgumentNullException_WhenNieuwsIsNull()
    {
        var mockRepo = new Mock<INieuwsberichtRepository>();
        var service = new NieuwsberichtService(mockRepo.Object);

        await Assert.ThrowsAsync<ArgumentNullException>(() => service.AddNieuwsberichtAsync(null));
    }

    // Test of AddNieuwsberichtAsync exceptions van de repository netjes doorgeeft
    [Fact]
    public async Task AddNieuwsberichtAsync_PropagatesExceptionFromRepository()
    {
        var mockRepo = new Mock<INieuwsberichtRepository>();
        mockRepo.Setup(r => r.AddAsync(It.IsAny<Nieuwsbericht>())).ThrowsAsync(new InvalidOperationException());

        var service = new NieuwsberichtService(mockRepo.Object);

        var nieuws = new Nieuwsbericht(0, "Titel", "Inhoud", DateTime.Now);

        await Assert.ThrowsAsync<InvalidOperationException>(() => service.AddNieuwsberichtAsync(nieuws));
    }
}
