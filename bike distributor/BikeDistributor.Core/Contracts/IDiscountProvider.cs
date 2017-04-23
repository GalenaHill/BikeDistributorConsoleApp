namespace BikeDistributor.Core.Contracts
{
    /// <summary>
    /// abstracts / encapsulates logic with regard to 
    /// discount processing as discounts can be processed both
    /// at the LineItem level or at the aggregate order level 
    /// </summary>
    public interface IDiscountProvider
    {
        decimal ScanLineItem(ILineItem lineItem);
        decimal ScanOrder(IOrder order);
    }
}