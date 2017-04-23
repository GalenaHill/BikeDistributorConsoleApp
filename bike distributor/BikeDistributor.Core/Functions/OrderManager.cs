namespace BikeDistributor.Core.Functions
{
    using System;
    using System.Text;
    using Contracts;
    using DomainObjects;
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

        public Order CalcualteOrder(Order incomingOrder)
        {
            incomingOrder.Subtotal = 0;

            // lines
            foreach (var line in incomingOrder.Lines)
            {
                line.TotalBeforeDiscount = line.Bike.Price * line.Quantity;

                line.DiscountCoefficient = this._discountProvider
                    .IssueAgingDiscount(line.Bike.DaysInInventory);

                line.DiscountAmount =
                    line.TotalBeforeDiscount * line.DiscountCoefficient;

                line.Total = line.TotalBeforeDiscount - line.DiscountAmount;

                incomingOrder.Subtotal += line.Total;
            }

            //rest
            incomingOrder.DiscountCoefficient =
                this._discountProvider.IssueVolumeDiscount(incomingOrder.Subtotal);

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

        public string GetReceipt(Order calculatedOrder, ReceiptType receiptType)
        {
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

        private string _getPlain(Order order)
        {
            var result = new StringBuilder($"Order Receipt for " +
                                           $"{order.Company}{Environment.NewLine}");


            foreach (var line in order.Lines)
            {

                result.AppendLine(
                    $"{line.Quantity} bikes of brand " +
                    $"{line.Bike.Brand} at " +
                    $"{line.DiscountCoefficient * 100} % disc at " +
                    $"{line.Bike.Price} each  = a pre-tax total of " +
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

        private string _getHtml(Order order)
        {
            throw new NotImplementedException(
                "Same thing here other than the html markup string - " +
                "going to skip this for now...");
        }

        #endregion
    }
}