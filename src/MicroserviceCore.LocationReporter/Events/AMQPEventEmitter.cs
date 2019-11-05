using System;
using System.Linq;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using MicroserviceCore.LocationReporter.Models;

namespace MicroserviceCore.LocationReporter.Events
{
    public class AMQPEventEmitter : IEventEmitter
    {
        private readonly ILogger logger;

        private AMQPOptions rabbitOptions;

        private ConnectionFactory connectionFactory;

        public AMQPEventEmitter(ILogger<AMQPEventEmitter> logger, IOptions<AMQPOptions> AMQPOptions)
        {
            this.logger = logger;
            this.rabbitOptions = AMQPOptions.Value;
            this.connectionFactory = new ConnectionFactory();

            connectionFactory.UserName = rabbitOptions.Username;
            connectionFactory.Password = rabbitOptions.Password;
            connectionFactory.VirtualHost = rabbitOptions.VirtualHost;
            connectionFactory.HostName = rabbitOptions.HostName;

            logger.LogInformation("AMQP Event Emitter configured with hostname", rabbitOptions.HostName);
        }

        public const string QUEUE_LOCATIONRECORD = "memberlocationrecorded";

        public void EmitLocationRecordedEvent(MemberLocationRecordedEvent locationRecordedEvent)
        {
            using (IConnection conn = connectionFactory.CreateConnection())
            {
                using (IModel channel = conn.CreateModel())
                {
                    channel.QueueDeclare(
                        queue: QUEUE_LOCATIONRECORD,
                        durable: false,
                        exclusive: false,
                        autoDelete: false,
                        arguments: null
                    );

                    string jsonPayload = locationRecordedEvent.toJson();
                    var body = Encoding.UTF8.GetBytes(jsonPayload);

                    channel.BasicPublish(
                        exchange: "",
                        routingKey: QUEUE_LOCATIONRECORD,
                        basicProperties: null,
                        body: body
                    );
                }
            }
        }
    }
}