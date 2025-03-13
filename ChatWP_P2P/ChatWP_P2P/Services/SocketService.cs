using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using ChatWP_P2P.Dtos;
using ChatWP_P2P.Entities;
using ChatWP_P2P.Enums;
using ChatWP_P2P.Helpers;

namespace ChatWP_P2P.Services
{
    public class SocketService
    {
        private readonly int _discoveryPort;
        public int Port { get; } = 5123;

        private readonly UnicastIPAddressInformation _ipv4Address;
        public string Ip { get; private set; }

        private UdpClient _discoveryClient = null!;
        private TcpListener _chatListener = null!;

        public Action<SocketResponse> OnMessageReceived { get; set; } = null!;

        public SocketService()
        {
            _discoveryPort = 51234;
            _ipv4Address = LoadInterfaz();
            Ip = _ipv4Address.Address.ToString();
        }

        public void Start(string name)
        {
            Task.Run(() => Connect(name));
            Task.Run(StartDiscoveryListener);
            Task.Run(StartChatListener);

            _ = new System.Threading.Timer(DiscoveryCallback, name, 0, 2500);
        }

        private void DiscoveryCallback(object? state)
        {
            string myName = (string)state!;
            Connect(myName);
        }

        private void StartDiscoveryListener()
        {
            var listener = new UdpClient(new IPEndPoint(IPAddress.Any, _discoveryPort));
            while (true)
            {
                var remoteEp = new IPEndPoint(IPAddress.Any, _discoveryPort);
                var receivedData = listener.Receive(ref remoteEp);
                var message = Encoding.UTF8.GetString(receivedData);

                var parts = message.Split('|');
                if (parts.Length != 3 || parts[1] == Ip || parts[2] != "Register") continue;
                Console.WriteLine($"[DISCOVERY] Nuevo usuario: {parts[0]} en {parts[1]}:{Port}");
                OnMessageReceived?.Invoke(new SocketResponse
                {
                    User = new UserConnection
                    {
                        Name = parts[0],
                        Host = parts[1],
                        Port = Port,
                    },
                    Message = "Account Created",
                    Type = MessageType.Register
                });
            }
        }

        private void Connect(string myName)
        {
            _discoveryClient = new UdpClient();
            _discoveryClient.EnableBroadcast = true;

            var broadcastAddress = NetworkHelper.GetBroadcastAddress(_ipv4Address.Address, _ipv4Address.IPv4Mask);
            var endPoint = new IPEndPoint(IPAddress.Parse(broadcastAddress), _discoveryPort);

            var discoveryMessage = $"{myName}|{Ip}|{MessageType.Register}";
            var data = Encoding.UTF8.GetBytes(discoveryMessage);

            _discoveryClient.Send(data, data.Length, endPoint);
            Console.WriteLine("[DISCOVERY] Mensaje enviado.");

        }

        private void StartChatListener()
        {
            _chatListener = new TcpListener(IPAddress.Any, Port);
            _chatListener.Start();
            Console.WriteLine("[CHAT] Esperando mensajes...");

            while (true)
            {
                try
                {
                    var client = _chatListener.AcceptTcpClient();
                    Task.Run(() => HandleChatClient(client));
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"[Chat] Error: {ex.Message}");
                }
            }
        }

        private void HandleChatClient(TcpClient client)
        {
            using var reader = new StreamReader(client.GetStream());
            try
            {
                while (client.Connected)
                {
                    var message = reader.ReadLine();
                    if (message is null) break;

                    var parts = message.Split("|");

                    if (parts.Length == 2)
                    {
                        OnMessageReceived?.Invoke(new SocketResponse
                        {
                            User = JsonSerializer.Deserialize<UserConnection>(parts[0])!,
                            Message = parts[1],
                            Type = parts[1] == MessageType.Close.ToString() ? MessageType.Close : MessageType.SendMessage
                        });
                    }

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[Chat] Error en recepción: {ex.Message}");
            }
            finally
            {
                client.Close();
            }
        }

        public void SendMessage(UserConnection user, string targetIp, int targetPort, string message)
        {
            try
            {
                using (TcpClient client = new TcpClient(targetIp, targetPort))
                using (NetworkStream stream = client.GetStream())
                using (StreamWriter writer = new StreamWriter(stream, Encoding.UTF8)) // Usar StreamWriter para enviar el mensaje
                {
                    writer.WriteLine($"{JsonSerializer.Serialize(user)}|{message}");
                    writer.Flush();
                    Console.WriteLine("[CHAT] Mensaje enviado.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("[CHAT] Error al enviar mensaje: " + ex.Message);
            }
        }

        private UnicastIPAddressInformation LoadInterfaz()
        {
            var networkInterface = NetworkInterface.GetAllNetworkInterfaces()
                .FirstOrDefault(nic => nic.OperationalStatus == OperationalStatus.Up &&
                                       nic.GetIPProperties().UnicastAddresses.Any(ip => ip.Address.AddressFamily == AddressFamily.InterNetwork));

            if (networkInterface is null)
                throw new Exception("Sin Interfaces activas");

            var information = networkInterface.GetIPProperties().UnicastAddresses
                      .FirstOrDefault(ip => ip.Address.AddressFamily == AddressFamily.InterNetwork);

            if (information is null)
                throw new Exception("Sin Informacion");

            return information;
        }
    }
}
