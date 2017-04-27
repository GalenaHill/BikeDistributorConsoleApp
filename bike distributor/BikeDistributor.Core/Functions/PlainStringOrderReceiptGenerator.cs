namespace BikeDistributor.Core.Functions
{
    using System;
    using System.Text;
    using Enums;
    using Contracts.domainObjects;
    using Contracts.functions;

    public class PlainStringOrderReceiptManager : IReceiptManager
    {
        public OrderPostSaleFunctionality Name { get; set; } = OrderPostSaleFunctionality.OrderReceiptInStringFormat;

        public dynamic GetReciept(object document)
        {
            var ord = (ISalesOrder)document;

            var result = new StringBuilder($"PLAIN STRING Order Receipt for " +
                                           $"{ord.CustomerInfo.CustomerName}{Environment.NewLine}");

            foreach (var line in ord.LineItems)
            {
                result.AppendLine(
                    $"{line.Quantity} products of brand " +
                    $"{line.ProductInfoItem.Brand} at " +
                    $"{line.GetTotalDiscount() * 100} % line level disc at " +
                    $"{line.ProductInfoItem.Price} each  = a pre-tax total of " +
                    $"{line.Total.ToString("C")}");
            }

            result.AppendLine($"Sub Total: {ord.Subtotal.ToString("C")}");

            result.AppendLine($"Additional order level discount at {ord.GetTotalDiscount() * 100} pct : " +
                              $"{(-ord.DiscountAmount).ToString("C")}");

            result.AppendLine($"Sub Total Net Discount: {ord.SubTotalNetDiscount.ToString("C")}");

            result.AppendLine($"Tax at {ord.TaxCoefficient * 100} %: {(ord.TaxAmount).ToString("C")}");

            result.AppendLine($"Order Total {ord.Total.ToString("C")}");

            return result.ToString();
        }
    }
}