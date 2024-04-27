using Server.Models;

namespace Server.DTOs
{
    public class ModelDto
    {
        public string Name { set; get; } = "";

        public Guid CategoryId { set; get; }
    }
}