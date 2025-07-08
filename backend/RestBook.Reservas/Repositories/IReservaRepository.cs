using RestBook.Reservas.Models;

namespace RestBook.Reservas.Repositories
{
    public interface IReservaRepository
    {
        Task<IEnumerable<Reserva>> GetAllAsync();
        Task<Reserva?> GetByIdAsync(int id);
        Task<Reserva> AddAsync(Reserva reserva);
        Task<bool> ExistsAsync(int id);
        Task SaveChangesAsync();
    }
}
