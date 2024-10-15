
using System.Collections.Generic;
using WebApiReportes.models;

namespace WebApiReportes.Repositories
{
    public interface ICargoRepository
    {
        //interface para el repositorio de cargo

        Task<IEnumerable<Cargo>> GetAllCargosAsync();
        Task<Cargo> GetCargoByIdAsync(int id);
        Task AddCargoAsync(Cargo cargo);
        Task UpdateCargoAsync(Cargo cargo);
        Task DeleteCargoAsync(int id);

    }
}
