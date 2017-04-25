namespace BikeDistributor.Core.Contracts.domainObjects
{
    /// <summary>
    /// encapsulates customer infomration relavent to 
    /// sales order processing
    /// </summary>
    public interface ICustomerInfo
    {
        string CustomerId { get; set; }
        string CustomerName { get; set; }
        string DiscountCode { get; set; }
    }
}