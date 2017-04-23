namespace BikeDistributor.Core.Functions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Contracts;

    public class DiscountProvider : IDiscountProvider
    {
        public DiscountProvider(IMockDiscountRepository discountRepository)
        {
            this._discountRepository = discountRepository;
        }

        private readonly IMockDiscountRepository _discountRepository;

        public decimal IssueAgingDiscount(int daysInInventory)
        {

            IDictionary<int, decimal> ageAmountDiscountPairs =
                this._discountRepository.GetAgeAmountDiscountPairs();

            if (ageAmountDiscountPairs == null)
            {
                throw new InvalidOperationException(
                    $"Could not retrieve ageAmountDiscountPairs, " +
                    $"{nameof(DiscountProvider)}");
            }

            // match key logic repetative and should encapsulate further
            #region match key logic
            List<int> matchKeys = new List<int>();

            foreach (var kvp in ageAmountDiscountPairs)
            {
                if (daysInInventory <= kvp.Key)
                {
                    matchKeys.Add(kvp.Key);
                }
            }

            if (!matchKeys.Any())
            {
                matchKeys.Add(ageAmountDiscountPairs.Last().Key);
            }

            int daysKey = matchKeys.Min(x => x);
            #endregion

            return ageAmountDiscountPairs.First(x => x.Key == daysKey).Value;
        }

        public decimal IssueVolumeDiscount(decimal grossSale)
        {
            IDictionary<decimal, decimal> grossSaleDiscountPairs =
                this._discountRepository.GetGrossSaleDiscountPairs();

            if (grossSaleDiscountPairs == null)
            {
                throw new InvalidOperationException(
                    $"Could not retrieve grossSaleDiscountPairs, " +
                    $"{nameof(DiscountProvider)}");
            }

            #region match key logic
            List<decimal> matchKeys = new List<decimal>();

            foreach (var kvp in grossSaleDiscountPairs)
            {
                if (grossSale <= kvp.Key)
                {
                    matchKeys.Add(kvp.Key);
                }
            }

            if (!matchKeys.Any())
            {
                matchKeys.Add(grossSaleDiscountPairs.Last().Key);
            }

            decimal grossSaleKey = matchKeys.Min(x => x);
            #endregion

            return grossSaleDiscountPairs.First(x => x.Key == grossSaleKey).Value;
        }
    }
}