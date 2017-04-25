namespace BikeDistributor.Core.Contracts.domainObjects
{
    using utilities;

    /// <summary>
    /// abstracts / encapsulates data points needed in a financial
    /// transaction involving and inventory item
    /// </summary>
    public interface ILineItem : IHasDiscountItem
    {
        IProductInfo ProductInfoItem { get; set; }

        decimal DiscountAmount { get; set; }

        decimal DiscountCoefficient { get; set; }

        int Quantity { get; set; }

        decimal Total { get; set; }

        decimal TotalBeforeDiscount { get; set; }
    }
}