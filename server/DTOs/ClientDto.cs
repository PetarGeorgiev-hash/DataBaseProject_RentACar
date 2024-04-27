namespace Server.DTOs
{
    public class ClientDto
    {
        public string Email { set; get; } = "";
        public string Name { set; get; } = "";

        public DateTime CreatedAt { set; get; } = DateTime.Now;
    }
}