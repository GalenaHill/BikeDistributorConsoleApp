namespace BikeDistributor.Core.DomainObjects
{
    using Contracts.domainObjects;

    public class DiscountItem : IDiscountItem
    {
        public string Name { get; set; }

        public decimal DiscountCoefficient { get; set; }
    }
}