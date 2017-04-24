using System;
using System.Text;
using BikeDistributor.Core.Enums;

namespace BikeDistributor.Core.Contracts
{
    /// <summary>
    /// generates reciept based on required reciept type, 
    /// incoming document and format type
    /// </summary>
    public interface IReceiptGenerator
    {
        ReceiptFormatType Name { get; set; }
        dynamic GetReciept(object incomingDocument);
    }

    public class PlainStringOrderReceiptGenerator : IReceiptGenerator
    {
        public ReceiptFormatType Name { get; set; } = ReceiptFormatType.PlainOrderFormat;

        public dynamic GetReciept(object document)
        {
            var ord = (IOrder)document;

            var result = new StringBuilder($"PLAIN STRING Order Receipt for " +
                                           $"{ord.CustomerSalesInfo.CustomerName}{Environment.NewLine}");

            foreach (var line in ord.LineItems)
            {
                result.AppendLine(
                    $"{line.Quantity} bikes of brand " +
                    $"{line.InventoryItem.Brand} at " +
                    $"{line.DiscountCoefficient * 100} % disc at " +
                    $"{line.InventoryItem.Price} each  = a pre-tax total of " +
                    $"{line.Total.ToString("C")}");
            }

            result.AppendLine($"Sub Total: {ord.Subtotal.ToString("C")}");

            result.AppendLine($"Additional discount at {ord.DiscountCoefficient * 100} pct : " +
                              $"{(-ord.DiscountAmount).ToString("C")}");

            result.AppendLine($"Sub Total Net Discount: {ord.SubTotalNetDiscount.ToString("C")}");

            result.AppendLine($"Tax at {ord.TaxCoefficient * 100} %: {(ord.TaxAmount).ToString("C")}");

            result.AppendLine($"Order Total {ord.Total.ToString("C")}");

            return result.ToString();
        }
    }

    public class MockOtherFormatOrderReceiptGenerator : IReceiptGenerator
    {
        public ReceiptFormatType Name { get; set; } = ReceiptFormatType.MockOtherOrderFormat;

        public dynamic GetReciept(object document)
        {
            var ord = (IOrder)document;

            var result = new StringBuilder($"SOME OTHER MOCK FORMAT Order Receipt for " +
                                           $"{ord.CustomerSalesInfo.CustomerName}{Environment.NewLine}");

            foreach (var line in ord.LineItems)
            {
                result.AppendLine(
                    $"{line.Quantity} bikes of brand " +
                    $"{line.InventoryItem.Brand} at " +
                    $"{line.DiscountCoefficient * 100} % disc at " +
                    $"{line.InventoryItem.Price} each  = a pre-tax total of " +
                    $"{line.Total.ToString("C")}");
            }

            result.AppendLine($"Sub Total: {ord.Subtotal.ToString("C")}");

            result.AppendLine($"Additional discount at {ord.DiscountCoefficient * 100} pct : " +
                              $"{(-ord.DiscountAmount).ToString("C")}");

            result.AppendLine($"Sub Total Net Discount: {ord.SubTotalNetDiscount.ToString("C")}");

            result.AppendLine($"Tax at {ord.TaxCoefficient * 100} %: {(ord.TaxAmount).ToString("C")}");

            result.AppendLine($"Order Total {ord.Total.ToString("C")}");

            return result.ToString();
        }
    }
}