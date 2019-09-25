using System;
using MicroserviceRabbitMQ.Controllers.Interfaces;
using MicroserviceRabbitMQ.Controllers;

namespace MicroserviceRabbitMQ
{
    class Inicio
    {
        private static readonly IEnviaController _enviaController = new EnviaController();
        private static readonly IRecebeController _recebeController = new RecebeController();

        public static void Main()
        {
            Menu();
        }

        static public void Menu()
        {
            Console.WriteLine("Escolha a opção desejada:");
            Console.WriteLine();
            Console.WriteLine("1 - Enviar mensagens");
            Console.WriteLine("2 - Receber mensagens");
            Console.WriteLine("3 - Sair");
            var result = Console.ReadLine();
            var opcao = int.Parse(result);
            switch (opcao)
            {
                case 1:
                    _enviaController.EnviarMensagem();
                    break;
                case 2:
                    _recebeController.ReceberMensagem();
                    break;
                case 3:
                    Environment.Exit(0);
                    break;
            }
        }
    }
}