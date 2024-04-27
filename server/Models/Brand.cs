namespace Server.Models
{
    public partial class Brand
    {
        public Guid Id { set; get; } = Guid.NewGuid();

        public string Name { set; get; } = "";

    }
}