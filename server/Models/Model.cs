namespace Server.Models
{
    public partial class Model
    {
        public Guid Id { set; get; } = Guid.NewGuid();
        public string Name { set; get; } = "";

        public Category? Category { set; get; }
    }
}