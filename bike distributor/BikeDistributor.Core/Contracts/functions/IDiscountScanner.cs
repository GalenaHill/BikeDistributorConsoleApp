namespace BikeDistributor.Core.Contracts.functions
{
    using domainObjects;
    using Enums;

    public interface IDiscountScanner
    {
        OrderScanLevel ScannerLevel { get; }

        void Scan(ISalesOrder incomingOrder);
    }
}