namespace BikeDistributor.Core.DomainObjects
{
    using Contracts.domain;

    public class Bike : IInventoryItem
    {
        public Bike()
        {
            
        }

        public Bike(
            string brand,
            string model,
            decimal price,
            int daysInInventory,
            string discountCode
            )
        {
            this.Brand = brand;
            this.Model = model;
            this.Price = price;
            this.DaysInInventory = daysInInventory;
            this.DiscountCode = discountCode;
        }

        public string Id { get; set; }

        public int DaysInInventory { get; set; }

        public string DiscountCode { get; set; }

        public string Brand { get; private set; }

        public string Model { get; private set; }

        public decimal Price { get; set; }

    }
}
