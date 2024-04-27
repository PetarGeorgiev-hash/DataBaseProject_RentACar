namespace Server.DTOs
{
    public partial class EditReportDto
    {
        public Guid Id { set; get; } = Guid.NewGuid();
        public Guid? ClientId { set; get; }

        public Guid? CarsForRentId { set; get; }

        public int DaysRented { set; get; }
    }
}