namespace BikeDistributor.Core.DomainObjects
{
    public class Bike
    {    
        public Bike(
            string brand, 
            string model, 
            decimal price, 
            int daysInInventory)
        {
            Brand = brand;
            Model = model;
            Price = price;
            DaysInInventory = daysInInventory;
        }

        public string Brand { get; private set; }
        public string Model { get; private set; }
        public decimal Price { get; set; }
        public int DaysInInventory { get; set; }
    }
}
