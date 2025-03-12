using ChatWP.Dtos;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace ChatWP.Controllers
{
    public class SocketController
    {
        public string Host { get; private set; } = String.Empty;
        public int Port { get; private set; } = 5000;
        private string _delimiter = "|";

        private TcpClient _client;
        private NetworkStream _stream;
        public Action<SocketResponse> OnMessageReceived { get; set; }


        public void ConnectToServer(string clientName, string host, int port)
        {
            Host = host;
            Port = port;
            try
            {
                _client = new TcpClient(host, Port);
                _stream = _client.GetStream();

                // Enviar el nombre del cliente para registrarse
                SendData(_stream, $"{clientName}{_delimiter}_{_delimiter}_{_delimiter}{MessageType.Register}");

                // Escuchar mensajes del servidor en un hilo separado
                Task.Run(ListenForMessages);

                Console.WriteLine("[CLIENT] Conectado al servidor.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[CLIENT] Error al conectar: {ex.Message}");
            }
        }

        private void ListenForMessages()
        {
            using var reader = new StreamReader(_stream);
            try
            {
                while (_client.Connected)
                {
                    string message = reader.ReadLine();
                    if (message is null) continue;

                    string[] parts = message.Split(_delimiter, 3);

                    MessageType type;
                    if (Enum.TryParse(parts[2], out type))
                    {
                        OnMessageReceived?.Invoke(new SocketResponse
                        {
                            FromUser = parts[0],
                            Data = parts[1],
                            Type = type
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[CLIENT] Error en recepción: {ex.Message}");
            }
            finally
            {
                Disconnect();
            }
        }

        public void SendMessage(string from, string to, string message, MessageType type)
        {
            try
            {
                if (_client == null || !_client.Connected) throw new Exception("No hay conexión con el servidor.");

                string formattedMessage = $"{from}{_delimiter}{to}{_delimiter}{message}{_delimiter}{type}";
                SendData(_stream, formattedMessage);
                Console.WriteLine("[CLIENT] Mensaje enviado.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[CLIENT] Error al enviar mensaje: {ex.Message}");
            }
        }

        public void SendMessageToExternalServer(string host, int port, string message)
        {
            try
            {
                using TcpClient externalClient = new TcpClient(host, port);
                using NetworkStream externalStream = externalClient.GetStream();

                string formattedMessage = $"{message}";
                SendData(externalStream, formattedMessage);

                Console.WriteLine($"[CLIENT] Mensaje enviado a {host}:{port}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[CLIENT] Error al enviar mensaje externo: {ex.Message}");
            }
        }

        private void SendData(NetworkStream stream, string data)
        {
            byte[] buffer = Encoding.UTF8.GetBytes(data + "\n"); // Agregar salto de línea para `ReadLine`
            stream.Write(buffer, 0, buffer.Length);
        }

        public void Disconnect()
        {
            _stream?.Close();
            _client?.Close();
            Console.WriteLine("[CLIENT] Desconectado del servidor.");
        }
    }
}
