using System;

namespace MicroserviceRabbitMQ.Model
{
    class Mensagem
    {
        public Guid RequisicaoGuid { get; set; } = Guid.NewGuid();
        public string Descricao { get; set; }
        public string IdentificadorServico { get; set; }
        public TimeSpan HoraEnvio { get; set; }
    }
}
