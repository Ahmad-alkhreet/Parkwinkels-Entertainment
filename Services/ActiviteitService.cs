using DataAccess.Repositories;
using Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service
{
    public class ActiviteitService
    {
        private readonly ActiviteitRepository _activiteitRepository;

        public ActiviteitService(ActiviteitRepository activiteitRepository)
        {
            _activiteitRepository = activiteitRepository;
        }

        public async Task<List<Activiteit>> GetAllActiviteitenAsync()
        {
            return await _activiteitRepository.GetAllAsync();
        }

        public async Task AddActiviteitAsync(string naam, string beschrijving, string locatie, DateTime start, DateTime einde, int max)
        {
            var activiteit = new Activiteit(0, naam, beschrijving, locatie, start, einde, max);
            await _activiteitRepository.AddAsync(activiteit);
        }
    }
}
