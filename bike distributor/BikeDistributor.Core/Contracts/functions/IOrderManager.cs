namespace BikeDistributor.Core.Contracts.functions
{
    using domainObjects;
    using Enums;

    /// <summary>
    /// abstracts / encapsulates the functionality required to
    /// process a sales order
    /// </summary>
    public interface IOrderManager
    {
        ISalesOrder CalcualteOrder(ISalesOrder incomingOrder);
        dynamic HandleOrderPostSale(ISalesOrder calculatedOrder, OrderPostSaleFunctionality orderPostSaleFunctionality);
    }
}