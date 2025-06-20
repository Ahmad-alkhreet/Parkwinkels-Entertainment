using Xunit;
using Moq;
using Domain;
using Service;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace UnitTest
{
    public class ActiviteitServiceTests
    {
        // 1. Test: controleren of GetAllActiviteitenAsync een lijst met activiteiten retourneert
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

        // 2. Test: controleren of AddActiviteitAsync de repository correct aanroept met de juiste activiteit
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

        // 3. Test: controleren of AddActiviteitAsync een ArgumentNullException gooit wanneer null wordt meegegeven
        [Fact]
        public async Task AddActiviteitAsync_ThrowsArgumentNullException_WhenActiviteitIsNull()
        {
            var mockRepo = new Mock<IActiviteitRepository>();
            var service = new ActiviteitService(mockRepo.Object);

            await Assert.ThrowsAsync<ArgumentNullException>(() => service.AddActiviteitAsync(null));
        }

        // 4. Test: controleren of GetAllActiviteitenAsync een lege lijst retourneert wanneer geen activiteiten aanwezig zijn
        [Fact]
        public async Task GetAllActiviteitenAsync_ReturnsEmptyList_WhenNoActivities()
        {
            var mockRepo = new Mock<IActiviteitRepository>();
            mockRepo.Setup(r => r.GetAllAsync()).ReturnsAsync(new List<Activiteit>());

            var service = new ActiviteitService(mockRepo.Object);

            var result = await service.GetAllActiviteitenAsync();

            Assert.Empty(result);
        }

        // 5. Test: controleren of GetAllActiviteitenAsync meerdere activiteiten teruggeeft
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

        // 6. Test: controleren of de constructor een exception gooit wanneer de naam leeg is
        [Fact]
        public void Constructor_ThrowsException_WhenNaamIsLeeg()
        {
            var ex = Assert.Throws<ArgumentException>(() =>
                new Activiteit(0, "", "beschrijving", "locatie", DateTime.Now, DateTime.Now.AddHours(1), 10));
            Assert.Equal("Naam mag niet leeg zijn.", ex.Message);
        }

        // 7. Test: controleren of de constructor een exception gooit wanneer starttijd gelijk of later is dan eindtijd
        [Fact]
        public void Constructor_ThrowsException_WhenStarttijdNaEindtijd()
        {
            var ex = Assert.Throws<ArgumentException>(() =>
                new Activiteit(0, "naam", "beschrijving", "locatie", DateTime.Now.AddHours(2), DateTime.Now, 10));
            Assert.Equal("Starttijd moet voor eindtijd liggen.", ex.Message);
        }

        // 8. Test: controleren of de constructor een exception gooit wanneer maxDeelnemers 0 of lager is
        [Fact]
        public void Constructor_ThrowsException_WhenMaxDeelnemersIsNul()
        {
            var ex = Assert.Throws<ArgumentException>(() =>
                new Activiteit(0, "naam", "beschrijving", "locatie", DateTime.Now, DateTime.Now.AddHours(1), 0));
            Assert.Equal("Maximaal aantal deelnemers moet groter zijn dan nul.", ex.Message);
        }

        // 9. Test: controleren of een geldig Activiteit-object correct wordt aangemaakt
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

        // 10. Test: controleren of exceptions van de repository correct worden doorgegeven door de service
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
}
