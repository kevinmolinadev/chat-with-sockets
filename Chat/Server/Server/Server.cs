using System.Collections.Concurrent;
using System.Net;
using System.Net.Sockets;
using System.Text.Json;
using Server.Entities;

namespace Server;

internal class Server
{
	private static bool _isRunning = true;
	private static int _port = 5000;
	private static string _delimiter = "|"; 
	private static ConcurrentDictionary<string, Client> _clients = new ConcurrentDictionary<string, Client>();
	
	public static async Task Main()
	{
		var server = new TcpListener(IPAddress.Any, _port);
		server.Start();
		Console.WriteLine($"[SERVER] Running on port {_port}...");
		await Task.Run(() =>
		{
			while (_isRunning)
			{
				try
				{
					var client = server.AcceptTcpClient();
					Console.WriteLine("[SERVER] Cliente conectado.");

					Task.Run(() => HandleClient(client));
				}
				catch (Exception ex)
				{
					Console.WriteLine($"[SERVER] Error: {ex.Message}");
				}
			}
		});
	}

	private static void BroadCastMessage(string message)
	{
		foreach (var kvp in _clients)
		{
			SendMessage(kvp.Value.TcpClient.GetStream(), message);	
		}
	}
	
	private static void SendMessage(NetworkStream stream, string message)
	{
		try
		{
			var writer = new StreamWriter(stream);
			writer.AutoFlush = true;
			writer.WriteLine(message);
		}
		catch (Exception ex)
		{
			Console.WriteLine($"[SERVER] Error al enviar mensaje: {ex.Message}");
		}
	}
	
	private static void HandleClient(TcpClient client)
	{
		var stream = client.GetStream();
		using var reader = new StreamReader(stream);
		string clientId = null!;
		try
		{
			while (client.Connected)
			{
				var data = reader.ReadLine();
				if (data == null) break;
				
				var parts = data.Split(_delimiter, 4);
				//[0] = FromId
				//[1] = ToId
				//[2] = Message
				//[3] = MessageType
				
				Console.WriteLine($"[SERVER] From: {clientId} To: {parts[1]} Message: {parts[2]}");
				
				if (!Guid.TryParse(parts[0], out _))
				{
					var newClient = new Client
					{
						Name = parts[0],
						TcpClient = client,
					};
					clientId = newClient.Id;

					var message = $"{newClient.Id}{_delimiter}{newClient.Name}{_delimiter}{parts[3]}";
					BroadCastMessage(message);
					_clients.TryAdd(clientId, newClient);
					SendMessage(stream,message);
					continue;
				}
				
				if (parts[2] == "/users")
				{
					var userList = _clients.Select(c => new { Id = c.Key, Name = c.Value.Name }).ToList();
					var jsonUsers = JsonSerializer.Serialize(userList);
					SendMessage(stream, $"{parts[0]}{_delimiter}{jsonUsers}{_delimiter}{parts[3]}");
					continue;
				}
				
				if (parts.Length == 4 && _clients.TryGetValue(parts[1], out var targetClient))
				{
					SendMessage(targetClient.TcpClient.GetStream(), $"{parts[0]}{_delimiter}{parts[2]}{_delimiter}{parts[3]}");
				}
			}
		}
		catch (Exception ex)
		{
			Console.WriteLine($"[SERVER] Error: {ex.Message}");
		}
		finally
		{
				_clients.TryRemove(clientId, out _);
				client.Close();
				Console.WriteLine($"[SERVER] Cliente {clientId} desconectado.");
		}
	}
}