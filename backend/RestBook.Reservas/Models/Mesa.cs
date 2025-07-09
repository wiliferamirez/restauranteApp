namespace RestBook.Reservas.Models
{
    public class Mesa
    {
        public int Id { get; set; }
        public int Capacidad { get; set; }
        public bool Disponible { get; set; }

        public ICollection<Reserva>? Reservas { get; set; }
    }
}
