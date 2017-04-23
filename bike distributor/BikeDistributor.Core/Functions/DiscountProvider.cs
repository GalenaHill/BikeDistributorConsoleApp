namespace BikeDistributor.Core.Functions
{
    using Contracts.domain;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Contracts;

    public class DiscountProvider : IDiscountProvider
    {
        public DiscountProvider(IDiscountRepository discountRepository)
        {
            this._discountRepository = discountRepository;
        }

        private readonly IDiscountRepository _discountRepository;

        public decimal ScanLineItem(ILineItem lineItem)
        {
            decimal ageDiscCoeff = _checkForAgeDiscount(
                lineItem.InventoryItem.DaysInInventory);

            decimal additionalDiscount = _checkForProductDiscountCode(
                lineItem.InventoryItem.DiscountCode);

            return ageDiscCoeff + additionalDiscount;
        }

        public decimal ScanOrder(IOrder order)
        {
            decimal volumeDiscCoeff = _checkForVolumeDiscount(order.Subtotal);

            decimal customerDiscount = _checkCustomerDiscount(order.CustomerSalesInfo.CustomerId);

            return volumeDiscCoeff + customerDiscount;
        }

        private decimal _checkCustomerDiscount(string customerId)
        {
            IDictionary<string, decimal> discountCodePairs =
                this._discountRepository.PrefferredCustomerDiscountPairs();

            if (discountCodePairs == null)
            {
                throw new InvalidOperationException(
                    $"Could not retrieve customerDiscountCodePairs, " +
                    $"{nameof(DiscountProvider)}");
            }

            decimal value = 0;

            try
            {
                discountCodePairs.TryGetValue(customerId, out value);
            }

            catch (Exception) //can swallow, zero is good here...
            {

                return value;
            }

            return value;
        }

        private decimal _checkForVolumeDiscount(decimal subtotal)
        {
            IDictionary<decimal, decimal> grossSaleDiscountPairs =
                this._discountRepository.VolumeDiscountPairs();

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
                if (subtotal <= kvp.Key)
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


        private decimal _checkForProductDiscountCode(string discountCode)
        {
            IDictionary<string, decimal> discountCodePairs =
                this._discountRepository.DiscountCodePairs();

            if (discountCodePairs == null)
            {
                throw new InvalidOperationException(
                    $"Could not retrieve discountCodePairs, " +
                    $"{nameof(DiscountProvider)}");
            }

            decimal value = 0;

            try
            {
                discountCodePairs.TryGetValue(discountCode, out value);
            }

            catch (Exception) //can swallow, zero is good here...
            {

                return value;
            }

            return value;
        }

        private decimal _checkForAgeDiscount(int daysInInventory)
        {
            IDictionary<int, decimal> ageAmountDiscountPairs =
                this._discountRepository.AgeDiscountPairs();

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
    }
}