namespace BikeDistributor.Core.Contracts
{
    using System.Collections.Generic;

    /// <summary>
    /// retrieves persisted discount management settings
    /// </summary>
    public interface IDiscountRepository
    {
        // key: days in inv, value: disc. coefficient
        IDictionary<int, decimal> AgeDiscountPairs();

        // key: gross sale, value: disc. coefficient
        IDictionary<decimal, decimal> VolumeDiscountPairs();

        // key: disc code, value: disc. coefficient
        IDictionary<string, decimal> DiscountCodePairs();

        // key: customerId, value: disc. coefficient
        IDictionary<string, decimal> PrefferredCustomerDiscountPairs();
    }
}