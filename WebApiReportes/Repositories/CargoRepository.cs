using WebApiReportes.models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApiReportes.context;

namespace WebApiReportes.Repositories
{
    public class CargoRepository : ICargoRepository
    {
        private readonly AppDbContext _context;

        public CargoRepository(AppDbContext context)
        {
             _context = context;
        }

        public async Task<IEnumerable<Cargo>> GetAllCargosAsync()
        {
            return await _context.Set<Cargo>().ToListAsync();
        }

        public async Task<Cargo> GetCargoByIdAsync(int id)
        {
            return await _context.Set<Cargo>().FindAsync(id);
        }

        public async Task AddCargoAsync(Cargo cargo)
        {
            await _context.Set<Cargo>().AddAsync(cargo);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateCargoAsync(Cargo cargo)
        {
            _context.Set<Cargo>().Update(cargo);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteCargoAsync(int id)
        {
            var cargo = await GetCargoByIdAsync(id);
            if (cargo != null)
            {
                _context.Set<Cargo>().Remove(cargo);
                await _context.SaveChangesAsync();
            }
        }
    }
}
