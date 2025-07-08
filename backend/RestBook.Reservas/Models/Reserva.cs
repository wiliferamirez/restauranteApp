namespace RestBook.Reservas.Models
{
    public class Reserva
    {
        public int Id { get; set; }
        public string NombreCliente { get; set; }
        public string CorreoCliente { get; set; }
        public DateTime FechaHora { get; set; }
        public int CantidadPersonas { get; set; }
        public int MesaId { get; set; }
        public Mesa? Mesa { get; set; }
    }
}
