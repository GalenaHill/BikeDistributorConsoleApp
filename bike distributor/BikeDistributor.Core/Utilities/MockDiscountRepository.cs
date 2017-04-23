namespace BikeDistributor.Core.Utilities
{
    using System.Collections.Generic;
    using Contracts;

    // completely separate layer in real life

    public class MockDiscountRepository : IMockDiscountRepository
    {
        public IDictionary<int, decimal> GetAgeAmountDiscountPairs()
        {
            return new Dictionary<int, decimal>()
            {
                [20] = .05M,
                [50] = .10M,
                [90] = .20M
            };
        }

        public IDictionary<decimal, decimal> GetGrossSaleDiscountPairs()
        {
            return new Dictionary<decimal, decimal>()
            {
                [1000M] = .05M,
                [5000M] = .10M,
                [10000M] = .20M
            };
        }
    }
}