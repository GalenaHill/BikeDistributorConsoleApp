namespace BikeDistributor.Core.Contracts
{
    using BikeDistributor.Core.DomainObjects;
    using BikeDistributor.Core.Enums;

    public interface IRecieptGenerator
    {
        string GenerateReciept(Order order, ReceiptType receiptType);
    }
}