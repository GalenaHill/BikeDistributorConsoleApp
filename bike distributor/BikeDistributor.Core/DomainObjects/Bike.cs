namespace BikeDistributor.Core.DomainObjects
{
    public class Bike
    {
        // no bueno:
        //public const int OneThousand = 1000;
        //public const int TwoThousand = 2000;
        //public const int FiveThousand = 5000;
    
        public Bike(string brand, string model, decimal price, int daysInInventory)
        {
            this.Brand = brand;
            this.Model = model;
            this.Price = price;
            this.DaysInInventory = daysInInventory;
        }

        public string Brand { get; private set; }
        public string Model { get; private set; }
        public decimal Price { get; set; }
        public int DaysInInventory { get; set; }
    }
}
