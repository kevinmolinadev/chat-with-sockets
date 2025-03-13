namespace ChatWP_P2P.Entities
{
    public class UserConnection
    {

        public required string Name { get; init; }

        public string PublicName { get; set; } = null!;

        public string Host { get; set; } = null!;

        public int Port { get; set; }


        public override string ToString()
        {
            return PublicName??Name;
        }
    }
}

    
