using Microsoft.EntityFrameworkCore;
using RestBook.Reservas.Data;
using RestBook.Reservas.Models;

namespace RestBook.Reservas.Repositories
{
    public class ReservaRepository : IReservaRepository
    {
        private readonly RestBookDbContext _context;

        public ReservaRepository(RestBookDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Reserva>> GetAllAsync()
        {
            return await _context.Reservas.Include(r => r.Mesa).ToListAsync();
        }

        public async Task<Reserva?> GetByIdAsync(int id)
        {
            return await _context.Reservas.Include(r => r.Mesa).FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task<Reserva> AddAsync(Reserva reserva)
        {
            await _context.Reservas.AddAsync(reserva);
            return reserva;
        }
        public async Task UpdateAsync(Reserva reserva)
        {
            _context.Reservas.Update(reserva);
        }

        public async Task DeleteAsync(Reserva reserva)
        {
            _context.Reservas.Remove(reserva);
        }


        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Reservas.AnyAsync(r => r.Id == id);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
