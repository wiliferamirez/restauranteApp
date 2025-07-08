using RestBook.Reservas.DTOs;

namespace RestBook.Reservas.Services
{
    public interface IReservaService
    {
        Task<IEnumerable<ReservaDto>> GetAllReservasAsync();
        Task<ReservaDto?> GetReservaByIdAsync(int id);
        Task<ReservaDto> CrearReservaAsync(CreateReservaDto dto);
    }
}
