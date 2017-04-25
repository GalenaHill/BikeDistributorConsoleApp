namespace BikeDistributor.Core.Contracts.utilities
{
    /// <summary>
    /// i have me an id...
    /// </summary>
    /// <typeparam name="TId"></typeparam>
    public interface IHasId<TId>
    {
        TId Id { get; set; }
    }
}