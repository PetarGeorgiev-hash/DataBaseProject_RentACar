namespace Server.DTOs
{
    public class CarEditDto
    {
        public Guid Id { set; get; } = Guid.NewGuid();

        public Guid? BrandId { set; get; }

        public Guid? ModelId { set; get; }

        public Guid? CarYearId { set; get; }

        public Guid? PriceId { set; get; }
    }
}