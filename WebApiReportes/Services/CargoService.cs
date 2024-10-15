
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiReportes.models;
using WebApiReportes.Repositories;

namespace WebApiReportes.Services
{
    public class CargoService: ICargoService     {
        private readonly ICargoRepository _cargoRepository;

        public CargoService(ICargoRepository cargoRepository)
        {
            _cargoRepository = cargoRepository;
        }

        public async Task<IEnumerable<Cargo>> GetAllCargosAsync()
        {
            return await _cargoRepository.GetAllCargosAsync();
        }

        public async Task<Cargo> GetCargoByIdAsync(int id)
        {
            return await _cargoRepository.GetCargoByIdAsync(id);
        }

        public async Task AddCargoAsync(Cargo cargo)
        {
            await _cargoRepository.AddCargoAsync(cargo);
        }

        public async Task UpdateCargoAsync(Cargo cargo)
        {
            await _cargoRepository.UpdateCargoAsync(cargo);
        }

        public async Task DeleteCargoAsync(int id)
        {
            await _cargoRepository.DeleteCargoAsync(id);
        }
    }
    
}
