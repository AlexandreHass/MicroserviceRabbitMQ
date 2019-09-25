using MicroserviceRabbitMQ.Model;
using Newtonsoft.Json.Linq;
using System;

namespace MicroserviceRabbitMQ.Controllers
{
    class BaseController
    {
        public static Mensagem MontarMensagem(string messageJson)
        {
            var objetoJson = JObject.Parse(messageJson);
            Mensagem mensagem = new Mensagem();
            mensagem.RequisicaoGuid = new Guid((string)objetoJson["RequisicaoGuid"]);
            mensagem.Descricao = (string)objetoJson["Descricao"];
            mensagem.IdentificadorServico = (string)objetoJson["IdentificadorServico"];
            mensagem.HoraEnvio = TimeSpan.Parse((string)objetoJson["HoraEnvio"]);
            return mensagem;
        }
    }
}
