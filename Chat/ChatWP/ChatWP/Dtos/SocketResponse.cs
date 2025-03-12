using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatWP.Dtos
{
    public record SocketResponse
    {
        public required string FromUser {get; init;}
        public required string Data { get; init; }

        public MessageType Type { get; init; }
    }
}
