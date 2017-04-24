namespace BikeDistributor.Core.Contracts
{
    using Enums;
    /// <summary>
    /// abstracts / encapsulates the functionality required to
    /// process a sales order
    /// </summary>
    public interface IOrderManager
    {
        IOrder CalcualteOrder(IOrder incomingOrder);
        dynamic GetReceipt(IOrder calculatedOrder, ReceiptFunctionality receiptFunctionality);
    }
}