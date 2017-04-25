using BikeDistributor.Core.Contracts.dataAccess;

namespace BikeDistributor.MockDataAccess
{
    using System.Collections.Generic;
    using Core.Contracts;

    // completely separate layer in real life

    public class MockDiscountRepository : IDiscountRepository
    {
        public IDictionary<string, decimal> PromoCodeDiscountPairs()
        {
            return new Dictionary<string, decimal>()
            {
                ["ABC-123"] = .05M,
                ["CDE-456"] = .10M,
                ["FGH-789"] = .20M
            };
        }

        public IDictionary<string, decimal> PrefferredCustomerDiscountPairs()
        {
            return new Dictionary<string, decimal>()
            {
                ["CustomerId-123"] = .05M,
                ["CustomerId-456"] = .10M,
                ["CustomerId-789"] = .20M
            };
        }
    }
}