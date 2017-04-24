namespace BikeDistributor.Core.Contracts
{
    using Enums;

    /// <summary>
    /// generates reciepts for any type and any specified format
    /// </summary>
    public interface IReceiptGenerator
    {
        ReceiptFormatType Name { get; set; }
        dynamic GetReciept(object incomingDocument);
    }
}
