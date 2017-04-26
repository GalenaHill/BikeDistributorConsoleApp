namespace BikeDistributor.Core.Functions
{
    using Contracts.domainObjects;
    using Contracts.functions;
    using Contracts.utilities;
    using System.Collections.Generic;
    using System.Linq;
    using System;
    using Enums;
    using ExtensionMethods;

    public class OrderManager : IOrderManager
    {
        public OrderManager(
            IAppSettings appSettings,
            Func<string, IEnumerable<IReceiptManager>> recGeneratorFactory, 
            IEnumerable<IDiscountScanner> discountScanners)
        {
            if (appSettings == null)
            {
                throw new ArgumentNullException(nameof(appSettings));
            }

            this._taxRate = appSettings.Get<decimal>("taxRate");
            this._receiptGeneratorFactory = recGeneratorFactory;
            this._discountScanners = discountScanners;
        }

        private readonly decimal _taxRate;
        private readonly Func<string, IEnumerable<IReceiptManager>> _receiptGeneratorFactory;
        private readonly IEnumerable<IDiscountScanner> _discountScanners;

        public ISalesOrder CalcualteOrder(ISalesOrder incomingOrder)
        {
            incomingOrder.Subtotal = 0;

            // line level discount scan
            foreach (var scanner in this._discountScanners
                .Where(x => x.ScannerLevel == OrderScanLevel.LineItemLevel))
            {
                scanner.Scan(incomingOrder);
            }

            // lines
            foreach (var line in incomingOrder.LineItems)
            {
                line.TotalBeforeDiscount = line.ProductInfoItem.Price * line.Quantity;

                line.DiscountAmount =
                    line.TotalBeforeDiscount * line.GetTotalDiscount();

                line.Total = line.TotalBeforeDiscount - line.DiscountAmount;

                incomingOrder.Subtotal += line.Total;
            }

            // order level discount scan
            foreach (var scanner in this._discountScanners
                .Where(x => x.ScannerLevel == OrderScanLevel.AggregateOrderLevel))
            {
                scanner.Scan(incomingOrder);
            }

            incomingOrder.DiscountAmount =
                incomingOrder.GetTotalDiscount() * incomingOrder.Subtotal;

            incomingOrder.SubTotalNetDiscount =
                incomingOrder.Subtotal - incomingOrder.DiscountAmount;

            incomingOrder.TaxCoefficient = this._taxRate;

            incomingOrder.TaxAmount =
                incomingOrder.SubTotalNetDiscount * this._taxRate;

            incomingOrder.Total =
                incomingOrder.SubTotalNetDiscount + incomingOrder.TaxAmount;

            return incomingOrder;
        }

        public dynamic GetReceipt(
            ISalesOrder calculatedOrder, 
            ReceiptFunctionality receiptFunctionality)
        {
            var generator =
                this._receiptGeneratorFactory(
                    Enum.GetName(typeof(ReceiptFunctionality), receiptFunctionality))
                    .FirstOrDefault(x => x.Name == receiptFunctionality);

            if (generator == null)
            {
                throw new InvalidOperationException(
                    $"BOOM! - get a receipt generator to match!!! " +
                    $"{nameof(this.GetType)}");
            }

            return generator.GetReciept(calculatedOrder);
        }
    }
}