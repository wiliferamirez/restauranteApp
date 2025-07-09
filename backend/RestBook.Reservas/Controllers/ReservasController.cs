using Microsoft.AspNetCore.Mvc;
using RestBook.Reservas.DTOs;
using RestBook.Reservas.Services;

namespace RestBook.Reservas.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReservasController : ControllerBase
    {
        private readonly IReservaService _reservaService;

        public ReservasController(IReservaService reservaService)
        {
            _reservaService = reservaService;
        }

        // GET: api/reservas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReservaDto>>> GetAll()
        {
            var reservas = await _reservaService.GetAllReservasAsync();
            return Ok(reservas);
        }

        // GET: api/reservas/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<ReservaDto>> GetById(int id)
        {
            var reserva = await _reservaService.GetReservaByIdAsync(id);
            if (reserva == null)
                return NotFound();

            return Ok(reserva);
        }

        // POST: api/reservas
        [HttpPost]
        public async Task<ActionResult<ReservaDto>> Create(CreateReservaDto dto)
        {
            var created = await _reservaService.CrearReservaAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }
        // PUT: api/reservas/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UpdateReservaDto dto)
        {
            var updated = await _reservaService.UpdateReservaAsync(id, dto);
            if (!updated) return NotFound();
            return NoContent();
        }
        // DELETE: api/reservas/{id}

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _reservaService.DeleteReservaAsync(id);
            if (!deleted) return NotFound();
            return NoContent();
        }

    }
}
