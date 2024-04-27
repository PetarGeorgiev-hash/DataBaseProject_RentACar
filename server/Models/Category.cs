namespace Server.Models
{
    public partial class Category
    {
        public Guid Id { set; get; } = Guid.NewGuid();
        public string Name { set; get; } = "";


    }
}