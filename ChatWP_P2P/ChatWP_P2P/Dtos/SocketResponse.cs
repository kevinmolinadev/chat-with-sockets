using ChatWP_P2P.Entities;
using ChatWP_P2P.Enums;

namespace ChatWP_P2P.Dtos
{
    public record SocketResponse
    {
        public UserConnection User { get; init; } = null!;
        public required string Message { get; init; }

        public MessageType Type { get; init; }
    }
}
