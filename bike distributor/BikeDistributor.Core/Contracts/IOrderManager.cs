using BikeDistributor.Core.Enums;

namespace BikeDistributor.Core.Contracts
{
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