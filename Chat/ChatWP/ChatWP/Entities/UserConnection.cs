namespace ChatWP.Entities
{
    public class UserConnection
    {
        public required string Id { get; init; }

        public required string Name { get; init; }

        public string PublicName { get; set; } = null!;

        public string Host { get; set; }

        public int Port { get; set; }


        public override string ToString()
        {
            return PublicName??Name;
        }
    }
}

    
