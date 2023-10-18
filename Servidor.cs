using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Application
{
    public class Servidor
    {
		// tamanho máximo das mensagens recebidas pelo servidor
		private const int TAMANHO_BUFFER = 65536;
		private int requisicoes; //nr. de mensagens

		//mensagem que o cliente manda para o servidor
		public string mensagemCliente;
		//mensagem que o servidor manda ao cliente
		public string respostaServidor;

		//Socket do servidor
		private TcpListener servidor;
		//Socket do cliente
		private TcpClient cliente;

		public Servidor(int porta)
        {
			//escuta tentativas de requisções do cliente
			this.servidor = new TcpListener(IPAddress.Any, porta);
			//fornece a conexão TCP
			this.cliente = default(TcpClient);
			//inicia o servidor
			this.servidor.Start();
			//aguarda por requisições do cliente
			this.cliente = servidor.AcceptTcpClient();
			this.requisicoes = 0;
			this.respostaServidor = "";

		}
		//código da thread servidor quando ouver requisições
		public void Run()
		{
			this.requisicoes++;
			//objeto para gerenciar o fluxo de dados durante a requisção
			NetworkStream netStream = cliente.GetStream();
			//variávrl para armazenar a mensagem recebida do cliente
			byte[] recebido = new byte[TAMANHO_BUFFER];
			//recebe a mensagem do cliente
			netStream.Read(recebido, 0, (int)cliente.ReceiveBufferSize);
			//converte bytes em string
			this.mensagemCliente = Encoding.ASCII.GetString(recebido);
			/* reduz a string deixando de fora os caracteres
	         * adicionados durante o processo de conversão bytes->string */
			this.mensagemCliente = this.mensagemCliente.Substring(0, this.mensagemCliente.IndexOf("$"));

			/* define a resposta do servidor
	         * manda para o cliente a mensagem recebida
	         * convertida em letras maiusculas */
			this.respostaServidor = "Resposta do Servidor " + Convert.ToString(requisicoes) + ": " +this.mensagemCliente.ToUpperInvariant();

			Byte[] enviado = Encoding.ASCII.GetBytes(this.respostaServidor);
			//envia a resposta em bytes ao cliente
			netStream.Write(enviado, 0, enviado.Length);
			netStream.Flush();
		}


	}
}
