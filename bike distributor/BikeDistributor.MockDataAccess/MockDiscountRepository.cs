namespace BikeDistributor.MockDataAccess
{
    using System.Collections.Generic;
    using Core.Contracts;

    // completely separate layer in real life

    public class MockDiscountRepository : IDiscountRepository
    {
        public IDictionary<int, decimal> AgeDiscountPairs()
        {
            return new Dictionary<int, decimal>()
            {
                [20] = .05M,
                [50] = .10M,
                [90] = .20M
            };
        }

        public IDictionary<decimal, decimal> VolumeDiscountPairs()
        {
            return new Dictionary<decimal, decimal>()
            {
                [1000M] = .05M,
                [5000M] = .10M,
                [10000M] = .20M
            };
        }

        public IDictionary<string, decimal> DiscountCodePairs()
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