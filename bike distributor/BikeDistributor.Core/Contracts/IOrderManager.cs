using BikeDistributor.Core.DomainObjects;
using BikeDistributor.Core.Enums;

namespace BikeDistributor.Core.Contracts
{
    public interface IOrderManager
    {
        Order CalcualteOrder(Order incomingOrder);
        string GetReceipt(Order calculatedOrder, ReceiptType receiptType);
    }
}