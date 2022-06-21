using Newtonsoft.Json;
using PersonApi.BL.Interfaces;
using PersonApi.Models;
using RabbitMQ.Client;
using System;
using System.Text;
using System.Threading.Tasks;

namespace PersonApi.BL.Services
{
    public class PersonService : IPersonService, IDisposable
    {   
        private readonly IModel _channel;
        private readonly IConnection _connection;

        public PersonService()
        {
            var factory = new ConnectionFactory() 
            {
                HostName = "localhost"
            };

            var connection = factory.CreateConnection();

            _channel = connection.CreateModel();
            _channel.ExchangeDeclare("person", ExchangeType.Fanout);
            _channel.QueueDeclare("person", true, false, false);
        }
        public void Dispose()
        {
            _channel.Dispose();
            _connection.Dispose();              
        }

        public async Task RegisterPersonASync(Person person)
        {
            await Task.Factory.StartNew(() =>
            {
                var serialize = JsonConvert.SerializeObject(person);
                var body = Encoding.UTF8.GetBytes(serialize);

                _channel.BasicPublish("", "person", body: body);
            });
        }
    }
}
