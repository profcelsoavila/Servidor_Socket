using System;
using Application;

namespace Servidor_Socket
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            //indica a porta de conexão
            const int PORTA = 9999;
            //cria o servidor
            Servidor servidor;
            servidor = new Servidor(PORTA);
            Console.WriteLine(">> Conexão estabelecida com cliente");

            while (true)
            {
                try
                {
                    //executa o servidor
                    servidor.Run();
                    //imprime as mensagens
                    Console.WriteLine(">> Dados do Cliente: " + servidor.mensagemCliente);
                    Console.WriteLine(">> Servidor envia: " + servidor.respostaServidor);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Exception: " + ex.Message);
                }
            }


        }
    }
}
