using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;

namespace HeroRegistrationApi
{
    public class Program
    {
        static void Main(string[] args)
        {
            var factory = new ConnectionFactory()
            {
                HostName = "localhost"
            };

            using (var connection = factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    channel.ExchangeDeclare("person", ExchangeType.Fanout);

                    channel.QueueDeclare("person", true, false, false);

                    var consumer = new EventingBasicConsumer(channel);

                    consumer.Received += (sender, ea) =>
                    {
                        var body = ea.Body.ToArray();

                        var message = Encoding.UTF8.GetString(body);

                        Console.WriteLine(message);
                    };

                    channel.BasicConsume(queue: "person", autoAck:true, consumer: consumer);
                    while (true)
                    {

                    }
                }
            }
        }
    }
}