namespace BikeDistributor.Core.Contracts
{
    using DomainObjects;
    using Enums;

    public interface IOrderManager
    {
        Order CalcualteOrder(Order incomingOrder);
        string GetReceipt(Order calculatedOrder, ReceiptType receiptType);
    }
}