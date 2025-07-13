using System.Text;
using System.Text.Json;
using RabbitMQ.Client;
using RestBook.Reservas.Models;

namespace RestBook.Reservas.Services
{
    public class RabbitMqPublisher
    {
        private readonly IConfiguration _configuration;

        public RabbitMqPublisher(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void PublishReservaCreada(Reserva reserva)
        {
            Console.WriteLine("Iniciando publicación a RabbitMQ...");

            var factory = new ConnectionFactory()
            {
                HostName = _configuration["RabbitMQ:Host"],
                Port = int.Parse(_configuration["RabbitMQ:Port"]),
                UserName = _configuration["RabbitMQ:Username"],
                Password = _configuration["RabbitMQ:Password"]
            };

            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();

            Console.WriteLine("Conectado a RabbitMQ");

            // Declaracion de cola
            channel.QueueDeclare(
                queue: "reserva_creada",
                durable: false,
                exclusive: false,
                autoDelete: false,
                arguments: null
            );

            Console.WriteLine("Cola 'reserva_creada' declarada.");

            // Serializar el mensaje
            var message = JsonSerializer.Serialize(new
            {
                reserva.Id,
                reserva.NombreCliente,
                reserva.CorreoCliente,
                reserva.FechaHora,
                reserva.CantidadPersonas,
                reserva.MesaId
            });

            var body = Encoding.UTF8.GetBytes(message);

            // Publicar mensaje
            channel.BasicPublish(
                exchange: "",
                routingKey: "reserva_creada",
                basicProperties: null,
                body: body
            );

            Console.WriteLine($"Mensaje enviado: {message}");

            // Espera para asegurar persistencia antes de cerrar
            Thread.Sleep(1000);
        }
    }
}
