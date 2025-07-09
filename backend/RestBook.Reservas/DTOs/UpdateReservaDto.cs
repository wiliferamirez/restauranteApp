namespace RestBook.Reservas.DTOs
{
    public class UpdateReservaDto
    {
        public string NombreCliente { get; set; } = string.Empty;
        public string CorreoCliente { get; set; } = string.Empty;
        public DateTime FechaHora { get; set; }
        public int CantidadPersonas { get; set; }
        public int MesaId { get; set; }
    }
}
