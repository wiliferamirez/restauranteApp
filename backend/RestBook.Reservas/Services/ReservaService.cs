using RestBook.Reservas.DTOs;
using RestBook.Reservas.Models;
using RestBook.Reservas.Repositories;

namespace RestBook.Reservas.Services
{
    public class ReservaService : IReservaService
    {
        private readonly IReservaRepository _reservaRepository;
        private readonly RabbitMqPublisher _rabbitMqPublisher;

        public ReservaService(IReservaRepository reservaRepository, RabbitMqPublisher rabbitMqPublisher)
        {
            _reservaRepository = reservaRepository;
            _rabbitMqPublisher = rabbitMqPublisher;
        }

        public async Task<IEnumerable<ReservaDto>> GetAllReservasAsync()
        {
            var reservas = await _reservaRepository.GetAllAsync();

            return reservas.Select(r => new ReservaDto
            {
                Id = r.Id,
                NombreCliente = r.NombreCliente,
                FechaHora = r.FechaHora,
                CantidadPersonas = r.CantidadPersonas,
                MesaInfo = $"Mesa #{r.MesaId} - Capacidad {r.Mesa?.Capacidad ?? 0}"
            });
        }

        public async Task<ReservaDto?> GetReservaByIdAsync(int id)
        {
            var reserva = await _reservaRepository.GetByIdAsync(id);
            if (reserva == null) return null;

            return new ReservaDto
            {
                Id = reserva.Id,
                NombreCliente = reserva.NombreCliente,
                FechaHora = reserva.FechaHora,
                CantidadPersonas = reserva.CantidadPersonas,
                MesaInfo = $"Mesa #{reserva.MesaId} - Capacidad {reserva.Mesa?.Capacidad ?? 0}"
            };
        }

        public async Task<ReservaDto> CrearReservaAsync(CreateReservaDto dto)
        {
            var nueva = new Reserva
            {
                NombreCliente = dto.NombreCliente,
                CorreoCliente = dto.CorreoCliente,
                FechaHora = dto.FechaHora,
                CantidadPersonas = dto.CantidadPersonas,
                MesaId = dto.MesaId
            };

            await _reservaRepository.AddAsync(nueva);
            await _reservaRepository.SaveChangesAsync();

            // Enviar evento a RabbitMQ
            _rabbitMqPublisher.PublishReservaCreada(nueva);

            return new ReservaDto
            {
                Id = nueva.Id,
                NombreCliente = nueva.NombreCliente,
                FechaHora = nueva.FechaHora,
                CantidadPersonas = nueva.CantidadPersonas,
                MesaInfo = $"Mesa #{nueva.MesaId}"
            };
        }

        public async Task<bool> UpdateReservaAsync(int id, UpdateReservaDto dto)
        {
            var reserva = await _reservaRepository.GetByIdAsync(id);
            if (reserva == null) return false;

            reserva.NombreCliente = dto.NombreCliente;
            reserva.CorreoCliente = dto.CorreoCliente;
            reserva.FechaHora = dto.FechaHora;
            reserva.CantidadPersonas = dto.CantidadPersonas;
            reserva.MesaId = dto.MesaId;

            await _reservaRepository.UpdateAsync(reserva);
            await _reservaRepository.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteReservaAsync(int id)
        {
            var reserva = await _reservaRepository.GetByIdAsync(id);
            if (reserva == null) return false;

            await _reservaRepository.DeleteAsync(reserva);
            await _reservaRepository.SaveChangesAsync();
            return true;
        }
    }
}
