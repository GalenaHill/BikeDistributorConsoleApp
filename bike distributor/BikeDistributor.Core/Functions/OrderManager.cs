namespace BikeDistributor.Core.Functions
{
    using System;
    using System.Text;
    using Contracts;
    using Enums;
    using ExtensionMethods;

    public class OrderManager : IOrderManager
    {
        public OrderManager(
            IDiscountProvider discountProvider, IAppSettings appSettings)
        {
            if (appSettings == null)
            {
                throw new ArgumentNullException(nameof(appSettings));
            }

            this._taxRate = appSettings.Get<decimal>("taxRate");

            this._discountProvider = discountProvider;
        }

        private readonly decimal _taxRate;

        private readonly IDiscountProvider _discountProvider;

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

        public string GetReceipt(IOrder calculatedOrder, ReceiptType receiptType)
        {
            // this needs further abstraction - IReceieptMaker etc...

            if (receiptType == ReceiptType.Plain)
            {
                return _getPlain(calculatedOrder);

            }
            else if (receiptType == ReceiptType.Html)

            {
                return _getHtml(calculatedOrder);
            }

            else
            {
                return "You ask for it - you got it - NO reciept for you!";
            }
        }

        #region privates

        private string _getPlain(IOrder order)
        {
            var result = new StringBuilder($"Order Receipt for " +
                                           $"{order.CustomerSalesInfo.CustomerName}{Environment.NewLine}");


            foreach (var line in order.LineItems)
            {

                result.AppendLine(
                    $"{line.Quantity} bikes of brand " +
                    $"{line.InventoryItem.Brand} at " +
                    $"{line.DiscountCoefficient * 100} % disc at " +
                    $"{line.InventoryItem.Price} each  = a pre-tax total of " +
                    $"{line.Total.ToString("C")}");
            }

            result.AppendLine($"Sub Total: {order.Subtotal.ToString("C")}");

            result.AppendLine($"Additional discount at {order.DiscountCoefficient * 100} pct : " +
                              $"{(-order.DiscountAmount).ToString("C")}");

            result.AppendLine($"Sub Total Net Discount: {order.SubTotalNetDiscount.ToString("C")}");

            result.AppendLine($"Tax at {_taxRate * 100} %: {(order.TaxAmount).ToString("C")}");

            result.AppendLine($"Order Total {order.Total.ToString("C")}");

            return result.ToString();
        }

        private string _getHtml(IOrder order)
        {
            throw new NotImplementedException(
                "Same thing here other than the html markup string - " +
                "going to skip this for now...");
        }

        #endregion
    }
}