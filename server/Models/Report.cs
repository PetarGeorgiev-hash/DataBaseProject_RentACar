namespace Server.Models
{
    public partial class Report
    {
        public Guid Id { set; get; } = Guid.NewGuid();

        public Client? Client { set; get; }

        public CarsForRent? CarsForRent { set; get; }

        public int DaysRented { set; get; }
    }
}