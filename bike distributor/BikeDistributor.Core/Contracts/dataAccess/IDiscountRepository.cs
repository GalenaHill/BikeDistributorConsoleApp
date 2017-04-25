using System.Collections.Generic;

namespace BikeDistributor.Core.Contracts.dataAccess
{
    /// <summary>
    /// retrieves persisted discount management settings
    /// </summary>
    public interface IDiscountRepository
    {
        // key: disc code, value: disc. coefficient
        IDictionary<string, decimal> PromoCodeDiscountPairs();

        // key: customerId, value: disc. coefficient
        IDictionary<string, decimal> PrefferredCustomerDiscountPairs();
    }
}