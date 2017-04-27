namespace BikeDistributor.Core.Contracts.functions
{
    using Enums;

    /// <summary>
    /// generates reciepts for any type and any specified format
    /// </summary>
    public interface IReceiptManager
    {
        OrderPostSaleFunctionality Name { get; set; }
        dynamic GetReciept(object incomingDocument);
    }
}
