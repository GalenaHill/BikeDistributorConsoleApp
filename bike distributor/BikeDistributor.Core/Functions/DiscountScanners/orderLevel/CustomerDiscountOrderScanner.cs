namespace BikeDistributor.Core.Functions.DiscountScanners.OrderLevel
{
    using System;
    using System.Collections.Generic;
    using Contracts.dataAccess;
    using Contracts.domainObjects;
    using Contracts.functions;
    using DomainObjects;
    using Enums;

    public class CustomerDiscountOrderScanner : IDiscountScanner
    {
        public CustomerDiscountOrderScanner(IDiscountRepository discountRepository)
        {
            this._discountRepository = discountRepository;
        }

        public OrderScanLevel ScannerLevel { get; } = OrderScanLevel.AggregateOrderLevel;
        private readonly IDiscountRepository _discountRepository;


        public void Scan(ISalesOrder incomingOrder)
        {
            IDictionary<string, decimal> discountCodePairs =
                this._discountRepository.PrefferredCustomerDiscountPairs();

            if (discountCodePairs == null)
            {
                throw new InvalidOperationException(
                    $"Could not retrieve customerDiscountCodePairs, " +
                    $"{nameof(CustomerDiscountOrderScanner)}");
            }

            decimal value = 0;

                if (!discountCodePairs
                    .TryGetValue(
                    incomingOrder.CustomerInfo.CustomerId, out value))
                {
                    return;
                }

            incomingOrder.AddDiscount(
                _addDiscountItem(
                    value, incomingOrder.CustomerInfo.CustomerId));
        }

        private IDiscountItem _addDiscountItem(decimal value, string code)
        {
            return new DiscountItem()
            {
                Name = nameof(CustomerDiscountOrderScanner)
                        + $"customer discount code discount :{code}",
                DiscountCoefficient = value
            };
        }
    }
}