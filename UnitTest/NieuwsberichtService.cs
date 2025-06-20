using Xunit;
using Moq;
using Domain;
using Service;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace UnitTest
{
    public class NieuwsberichtTests
    {
        // --- Domeinvalidatie tests ---

        // Test of de constructor een ArgumentException gooit als titel leeg of whitespace is
        [Fact]
        public void Constructor_ThrowsException_WhenTitelIsLeeg()
        {
            var ex = Assert.Throws<ArgumentException>(() =>
                new Nieuwsbericht(0, "", "inhoud", DateTime.Now));
            Assert.Contains("Titel mag niet leeg zijn.", ex.Message);
        }

        // Test of de constructor een ArgumentException gooit als inhoud leeg of whitespace is
        [Fact]
        public void Constructor_ThrowsException_WhenInhoudIsLeeg()
        {
            var ex = Assert.Throws<ArgumentException>(() =>
                new Nieuwsbericht(0, "titel", "   ", DateTime.Now));
            Assert.Contains("Inhoud mag niet leeg zijn.", ex.Message);
        }

        // Test of de constructor een ArgumentException gooit als publicatiedatum in de toekomst ligt
        [Fact]
        public void Constructor_ThrowsException_WhenPublicatiedatumInDeToekomst()
        {
            var toekomst = DateTime.Now.AddDays(1);
            var ex = Assert.Throws<ArgumentException>(() =>
                new Nieuwsbericht(0, "titel", "inhoud", toekomst));
            Assert.Contains("Publicatiedatum mag niet in de toekomst liggen.", ex.Message);
        }

        // Test of de constructor een geldig Nieuwsbericht-object aanmaakt met correcte properties
        [Fact]
        public void Constructor_CreatesObject_WhenValidInput()
        {
            var nu = DateTime.Now;
            var nieuws = new Nieuwsbericht(0, "titel", "inhoud", nu);

            Assert.Equal("titel", nieuws.Titel);
            Assert.Equal("inhoud", nieuws.Inhoud);
            Assert.Equal(nu, nieuws.Publicatiedatum);
        }
    }

    public class NieuwsberichtServiceTests
    {
        // Test of AddNieuwsberichtAsync een ArgumentNullException gooit wanneer null wordt doorgegeven
        [Fact]
        public async Task AddNieuwsberichtAsync_ThrowsArgumentNullException_WhenNieuwsIsNull()
        {
            var mockRepo = new Mock<INieuwsberichtRepository>();
            var service = new NieuwsberichtService(mockRepo.Object);

            await Assert.ThrowsAsync<ArgumentNullException>(() => service.AddNieuwsberichtAsync(null));
        }

        // Test of AddNieuwsberichtAsync de repository correct aanroept met een geldig nieuwsbericht
        [Fact]
        public async Task AddNieuwsberichtAsync_CallsRepositoryWithValidNieuwsbericht()
        {
            var mockRepo = new Mock<INieuwsberichtRepository>();
            var service = new NieuwsberichtService(mockRepo.Object);

            var nieuws = new Nieuwsbericht(0, "Goede titel", "Goede inhoud", DateTime.Now);

            await service.AddNieuwsberichtAsync(nieuws);

            // Controleer dat AddAsync precies één keer wordt aangeroepen met het juiste nieuwsbericht
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

            // Verwacht precies één item terug
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

            // Verwacht een lege lijst
            Assert.Empty(result);
        }

        // Test of AddNieuwsberichtAsync exceptions die vanuit de repository komen correct doorgeeft
        [Fact]
        public async Task AddNieuwsberichtAsync_PropagatesExceptionFromRepository()
        {
            var mockRepo = new Mock<INieuwsberichtRepository>();
            mockRepo.Setup(r => r.AddAsync(It.IsAny<Nieuwsbericht>())).ThrowsAsync(new InvalidOperationException());

            var service = new NieuwsberichtService(mockRepo.Object);

            var nieuws = new Nieuwsbericht(0, "Titel", "Inhoud", DateTime.Now);

            // Verwacht dat de exception wordt doorgegeven
            await Assert.ThrowsAsync<InvalidOperationException>(() => service.AddNieuwsberichtAsync(nieuws));
        }
    }
}
