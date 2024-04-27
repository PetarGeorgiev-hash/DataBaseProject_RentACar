namespace Server.Models
{
    public partial class CarsForRent
    {
        public Guid Id { set; get; } = Guid.NewGuid();

        public Brand? Brand { set; get; }

        public Model? Model { set; get; }

        public CarYear? CarYear { set; get; }

        public Price? Price { set; get; }

        public string Test { set; get; } = "";
    }
}