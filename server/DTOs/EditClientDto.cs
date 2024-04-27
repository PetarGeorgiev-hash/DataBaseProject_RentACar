namespace Server.DTOs
{
    public class EditClientDto
    {

        public Guid Id { set; get; } = Guid.NewGuid();
        public string Email { set; get; } = "";
        public string Name { set; get; } = "";

        public DateTime CreatedAt { set; get; } = DateTime.Now;
    }
}