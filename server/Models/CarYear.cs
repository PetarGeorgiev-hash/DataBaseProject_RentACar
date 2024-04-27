namespace Server.Models
{
    public partial class CarYear
    {
        public Guid Id { set; get; } = Guid.NewGuid();
        public DateTime Date { set; get; } = DateTime.Now;

    }
}