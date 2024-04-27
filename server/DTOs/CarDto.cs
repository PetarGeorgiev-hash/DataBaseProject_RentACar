using Server.Models;

namespace Server.DTOs
{
    public class CarDto
    {

        public Guid BrandId { set; get; }

        public Guid ModelId { set; get; }

        public Guid CarYearId { set; get; }

        public Guid PriceId { set; get; }
    }
}