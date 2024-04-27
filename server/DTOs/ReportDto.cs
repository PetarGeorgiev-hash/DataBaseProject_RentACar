namespace Server.DTOs
{
    public partial class ReportDto
    {

        public Guid? ClientId { set; get; }

        public Guid? CarsForRentId { set; get; }

        public int DaysRented { set; get; }
    }
}