namespace BikeDistributor.Core.Functions
{
    using System.Collections.Generic;
    using System.Linq;
    using System;
    using Contracts;
    using Enums;
    using ExtensionMethods;

    public class OrderManager : IOrderManager
    {
        public OrderManager(
            IDiscountProvider discountProvider,
            IAppSettings appSettings,
            Func<string, IEnumerable<IReceiptGenerator>> recGeneratorFactory)
        {
            if (appSettings == null)
            {
                throw new ArgumentNullException(nameof(appSettings));
            }

            this._taxRate = appSettings.Get<decimal>("taxRate");
            this._discountProvider = discountProvider;
            this._receiptGeneratorFactory = recGeneratorFactory;
        }

        private readonly decimal _taxRate;
        private readonly IDiscountProvider _discountProvider;
        private readonly Func<string, IEnumerable<IReceiptGenerator>> _receiptGeneratorFactory;

        public IOrder CalcualteOrder(IOrder incomingOrder)
        {
            incomingOrder.Subtotal = 0;

            // lines
            foreach (var line in incomingOrder.LineItems)
            {
                line.TotalBeforeDiscount = line.InventoryItem.Price * line.Quantity;

                line.DiscountCoefficient = this._discountProvider
                    .ScanLineItem(line);

                line.DiscountAmount =
                    line.TotalBeforeDiscount * line.DiscountCoefficient;

                line.Total = line.TotalBeforeDiscount - line.DiscountAmount;

                incomingOrder.Subtotal += line.Total;
            }

            //rest
            incomingOrder.DiscountCoefficient =
                this._discountProvider.ScanOrder(incomingOrder) +
                incomingOrder.ManualDiscountCoefficient;

            incomingOrder.DiscountAmount =
                incomingOrder.DiscountCoefficient * incomingOrder.Subtotal;

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
            IOrder calculatedOrder, ReceiptFormatType receiptFormatType)
        {
            var generator =
                this._receiptGeneratorFactory(
                    Enum.GetName(typeof(ReceiptFormatType), receiptFormatType))
                    .FirstOrDefault(x => x.Name == receiptFormatType);

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