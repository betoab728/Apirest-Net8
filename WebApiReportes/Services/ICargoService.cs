using System.Collections.Generic;
using System.Threading.Tasks;
using WebApiReportes.models;

namespace WebApiReportes.Services
{
    public interface ICargoService
    {
        Task<IEnumerable<Cargo>> GetAllCargosAsync();
        Task<Cargo> GetCargoByIdAsync(int id);
        Task AddCargoAsync(Cargo cargo);
        Task UpdateCargoAsync(Cargo cargo);
        Task DeleteCargoAsync(int id);
    }
}
