namespace BikeDistributor.Core.Contracts.domainObjects
{
    using utilities;

    /// <summary>
    /// models a simple inventory item
    /// </summary>
    public interface IProductInfo : IHasId<string>
    {
        string Brand { get; }

        string DiscountCode { get; set; }

        string Model { get; }

        decimal Price { get; set; }

        // lots more here in the real world
    }
}