namespace BikeDistributor.Core.Functions.DiscountScanners.LineItemLevel
{
    using System;
    using System.Collections.Generic;
    using Contracts.dataAccess;
    using Contracts.domainObjects;
    using Contracts.functions;
    using DomainObjects;
    using Enums;

    public class ProductPromoOrderScanner : IDiscountScanner
    {
        public ProductPromoOrderScanner(IDiscountRepository discountRepository)
        {
            this._discountRepository = discountRepository;
        }

        private readonly IDiscountRepository _discountRepository;
        public OrderScanLevel ScannerLevel { get; } = OrderScanLevel.LineItemLevel;

        public void Scan(ISalesOrder incomingOrder)
        {
            foreach (var line in incomingOrder.LineItems)
            {
                IDictionary<string, decimal> discountCodePairs =
                    this._discountRepository.PromoCodeDiscountPairs();

                if (discountCodePairs == null)
                {
                    throw new InvalidOperationException(
                        $"Could not retrieve discountCodePairs, " +
                        $"{nameof(ProductPromoOrderScanner)}");
                }

                decimal value = 0;

                try
                {
                    discountCodePairs.TryGetValue(line.ProductInfoItem.DiscountCode, out value);
                }

                catch (Exception) //can swallow, zero is good here...
                {
                    line.AddDiscount(
                        _addDiscountItem(
                            value, line.ProductInfoItem.DiscountCode));
                }

                line.AddDiscount(
                    _addDiscountItem(
                        value, line.ProductInfoItem.DiscountCode));
            }
        }

        private IDiscountItem _addDiscountItem(decimal value, string code)
        {
            return new DiscountItem()
            {
                Name = nameof(ProductPromoOrderScanner) 
                        + $"promo code discount :{code}",
                DiscountCoefficient = value
            };
        }
    }
}