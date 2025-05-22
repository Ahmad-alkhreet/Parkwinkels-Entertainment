using DataAccess.Repositories;
using Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service
{
    public class NieuwsberichtService
    {
        private readonly NieuwsberichtRepository _nieuwsRepository;

        public NieuwsberichtService(NieuwsberichtRepository nieuwsRepository)
        {
            _nieuwsRepository = nieuwsRepository;
        }

        public async Task<List<Nieuwsbericht>> GetAllNieuwsAsync()
        {
            return await _nieuwsRepository.GetAllAsync();
        }

        public async Task AddNieuwsberichtAsync(string titel, string inhoud)
        {
            var nieuws = new Nieuwsbericht(0, titel, inhoud, DateTime.Now);
            await _nieuwsRepository.AddAsync(nieuws);
        }
    }
}
