namespace BikeDistributor.Core.Contracts.domain
{
    /// <summary>
    /// encapsulates customer infomration relavent to 
    /// sales order processing
    /// </summary>
    public interface ICustomerSalesInfo
    {
        string CustomerId { get; set; }
        string CustomerName { get; set; }
        string DiscountCode { get; set; }
    }
}