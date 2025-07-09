namespace RestBook.Reservas.DTOs
{
    public class CreateReservaDto
    {
        public string NombreCliente { get; set; }
        public string CorreoCliente { get; set; }
        public DateTime FechaHora { get; set; }
        public int CantidadPersonas { get; set; }
        public int MesaId { get; set; }
    }
}
