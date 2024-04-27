using Server.Models;

namespace Server.DTOs
{
    public class EditModelDto
    {

        public Guid Id { set; get; } = Guid.NewGuid();
        public string Name { set; get; } = "";

        public Guid CategoryId { set; get; }
    }
}