namespace BikeDistributor.Core.DomainObjects
{
    using System.Collections.Generic;
    using Contracts.domainObjects;
    
    public class BikeProductInfo : IProductInfo
    {
        public BikeProductInfo()
        {

        }

        public BikeProductInfo(
            string brand,
            string model,
            decimal price,
            string discountCode
            )
        {
            this.Brand = brand;
            this.Model = model;
            this.Price = price;
            this.DiscountCode = discountCode;
        }

        public string Id { get; set; }

        public string DiscountCode { get; set; }

        public decimal ManualDiscount { get; set; }

        public string Brand { get; set; }

        public string Model { get; set; }

        public decimal Price { get; set; }

        public ICollection<IDiscountItem> DiscountItems { get; set; }

    }
}
