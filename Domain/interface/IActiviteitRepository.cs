using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain
{
    public interface IActiviteitRepository
    {
        Task<List<Activiteit>> GetAllAsync();
        Task AddAsync(Activiteit activiteit);
    }
}
