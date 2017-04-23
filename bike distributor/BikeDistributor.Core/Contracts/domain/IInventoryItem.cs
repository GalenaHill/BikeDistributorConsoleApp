namespace BikeDistributor.Core.Contracts.domain
{
    /// <summary>
    /// models a simple inventory item
    /// </summary>
    public interface IInventoryItem : IHasId<string>
    {
        string Brand { get; }

        int DaysInInventory { get; set; }

        string DiscountCode { get; set; }

        string Model { get; }

        decimal Price { get; set; }

        // lots more here in the real world
    }
}