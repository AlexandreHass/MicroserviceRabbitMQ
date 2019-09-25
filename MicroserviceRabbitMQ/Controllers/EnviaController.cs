using MicroserviceRabbitMQ.Controllers.Interfaces;
using MicroserviceRabbitMQ.Model;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System;
using System.Reflection;
using System.Text;
using System.Threading;

namespace MicroserviceRabbitMQ.Controllers
{
    class EnviaController : IEnviaController
    {

        public void EnviarMensagem()
        {
            while (true)
            {
                var factory = new ConnectionFactory() { HostName = "localhost" };
                using (var connection = factory.CreateConnection())
                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare(queue: "hello",
                                         durable: false,
                                         exclusive: false,
                                         autoDelete: false,
                                         arguments: null);

                    Mensagem mensagem = new Mensagem
                    {
                        Descricao = "Hello World",
                        HoraEnvio = DateTime.Now.TimeOfDay,
                        IdentificadorServico = Assembly.GetExecutingAssembly().GetName().Name
                    };

                    var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(mensagem));

                    channel.BasicPublish(exchange: "",
                                         routingKey: "hello",
                                         basicProperties: null,
                                         body: body);
                    Console.WriteLine("Mensagem {0} enviada.", mensagem.RequisicaoGuid);
                }
                Thread.Sleep(5000); // uma mensagem enviada a cada 5 segundos
            }
        }
    }
}
