namespace BikeDistributor.Core.Contracts.domainObjects
{
    public interface IDiscountItem
    {
        string Name { get; set; }
        decimal DiscountCoefficient { get; set; }
    }
}