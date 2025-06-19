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

        public async Task AddActiviteitAsync(Activiteit activiteit)
        {
            if (activiteit == null)
                throw new ArgumentNullException(nameof(activiteit));

            await _activiteitRepository.AddAsync(activiteit);
        }
    }
}
