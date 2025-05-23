using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain
{
    public interface INieuwsberichtRepository
    {
        Task<List<Nieuwsbericht>> GetAllAsync();
        Task AddAsync(Nieuwsbericht nieuws);
    }
}
