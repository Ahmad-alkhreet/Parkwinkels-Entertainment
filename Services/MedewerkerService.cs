using DataAccess.Repositories;
using Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service
{
    public class MedewerkerService
    {
        private readonly MedewerkerRepository _medewerkerRepository;

        public MedewerkerService(MedewerkerRepository medewerkerRepository)
        {
            _medewerkerRepository = medewerkerRepository;
        }

        public async Task<List<Medewerker>> GetAllMedewerkersAsync()
        {
            return await _medewerkerRepository.GetAllAsync();
        }

        public async Task AddMedewerkerAsync(string naam, string rol, string email)
        {
            var medewerker = new Medewerker(0, naam, rol, email);
            await _medewerkerRepository.AddAsync(medewerker);
        }
    }
}
