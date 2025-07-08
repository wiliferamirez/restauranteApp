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
            var factory = new ConnectionFactory()
            {
                HostName = _configuration["RabbitMQ:Host"] ?? "localhost",
                Port = int.Parse(_configuration["RabbitMQ:Port"] ?? "5672")
            };

            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();

            channel.QueueDeclare(queue: "reserva_creada",
                                 durable: false,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);

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

            channel.BasicPublish(exchange: "",
                                 routingKey: "reserva_creada",
                                 basicProperties: null,
                                 body: body);
        }
    }
}
