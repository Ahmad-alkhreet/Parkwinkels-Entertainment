using Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service
{
    public class ActiviteitService
    {
        private readonly IActiviteitRepository _activiteitRepository;

        public ActiviteitService(IActiviteitRepository activiteitRepository)
        {
            _activiteitRepository = activiteitRepository;
        }

        public async Task<List<Activiteit>> GetAllActiviteitenAsync()
        {
            return await _activiteitRepository.GetAllAsync();
        }

        public async Task AddActiviteitAsync(string naam, string beschrijving, string locatie, DateTime start, DateTime einde, int max)
        {
            if (string.IsNullOrWhiteSpace(naam))
                throw new ArgumentException("Naam mag niet leeg zijn.");

            if (string.IsNullOrWhiteSpace(beschrijving))
                throw new ArgumentException("Beschrijving mag niet leeg zijn.");

            if (string.IsNullOrWhiteSpace(locatie))
                throw new ArgumentException("Locatie mag niet leeg zijn.");

            if (start >= einde)
                throw new ArgumentException("Starttijd moet vóór eindtijd liggen.");

            if (max <= 0)
                throw new ArgumentException("Maximaal aantal deelnemers moet groter zijn dan 0.");


            var activiteit = new Activiteit(0, naam, beschrijving, locatie, start, einde, max);
            await _activiteitRepository.AddAsync(activiteit);
        }


    }
}
