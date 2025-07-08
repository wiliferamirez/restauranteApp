namespace RestBook.Reservas.DTOs
{
    public class ReservaDto
    {
        public int Id { get; set; }
        public string NombreCliente { get; set; }
        public DateTime FechaHora { get; set; }
        public int CantidadPersonas { get; set; }
        public string MesaInfo { get; set; }
    }
}
