using MicroserviceRabbitMQ.Controllers.Interfaces;
using MicroserviceRabbitMQ.Model;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;

namespace MicroserviceRabbitMQ.Controllers
{
    class RecebeController : BaseController, IRecebeController
    {
        public void ReceberMensagem()
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

                var consumer = new EventingBasicConsumer(channel);

                consumer.Received += (model, ea) =>
                {
                    var message = Encoding.UTF8.GetString(ea.Body);
                    var messageJson = JsonConvert.DeserializeObject(message);
                    Mensagem mensagem = MontarMensagem(messageJson.ToString());
                    Console.WriteLine("\nMensagem recebida:\nIdentificador: {0}\nMensagem: {1}\nEnviada do Microserviço: {2}\nHora: {3}\n\n",
                        mensagem.RequisicaoGuid, mensagem.Descricao, mensagem.IdentificadorServico, mensagem.HoraEnvio);
                };
                channel.BasicConsume(queue: "hello",
                                     autoAck: true,
                                     consumer: consumer);
                Console.WriteLine("\nAguardando mensagens...\n");
                Console.Read();
            }
        }
    }
}
