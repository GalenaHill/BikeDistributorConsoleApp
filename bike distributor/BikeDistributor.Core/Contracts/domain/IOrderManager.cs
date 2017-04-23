namespace BikeDistributor.Core.Contracts.domain
{
    using Enums;

    /// <summary>
    /// abstracts / encapsulates the functionality required to
    /// process a sales order
    /// </summary>
    public interface IOrderManager
    {
        IOrder CalcualteOrder(IOrder incomingOrder);
        string GetReceipt(IOrder calculatedOrder, ReceiptType receiptType);
    }
}