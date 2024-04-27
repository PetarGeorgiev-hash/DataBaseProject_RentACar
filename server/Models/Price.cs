namespace Server.Models
{
    public partial class Price
    {
        public Guid Id { set; get; } = Guid.NewGuid();

        public decimal CarPrice { set; get; }

    }
}