namespace BikeDistributor.Core.Contracts
{
    public interface IDiscountProvider
    {
        decimal IssueAgingDiscount(int daysInInventory);
        decimal IssueVolumeDiscount(decimal grossSale);
    }
}