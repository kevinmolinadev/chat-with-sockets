using System.Net.Sockets;

namespace Server.Entities;

public class Client
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public required string Name { get; set; }

    public override string ToString()
    {
        return $"{Id} {Name}";
    }
    
    public required TcpClient TcpClient { get; set; }
}