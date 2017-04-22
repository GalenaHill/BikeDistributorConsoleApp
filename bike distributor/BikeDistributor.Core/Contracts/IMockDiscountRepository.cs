namespace BikeDistributor.Core.Contracts
{
    using System.Collections.Generic;

    public interface IMockDiscountRepository
    {
        IDictionary<int, decimal> GetAgeAmountDiscountPairs();
        IDictionary<decimal, decimal> GetGrossSaleDiscountPairs();
    }
}