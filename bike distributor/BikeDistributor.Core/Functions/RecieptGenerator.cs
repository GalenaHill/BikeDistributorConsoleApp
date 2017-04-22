namespace BikeDistributor.Core.Functions
{
    using BikeDistributor.Core.ExtensionMethods;
    using BikeDistributor.Core.Contracts;
    using BikeDistributor.Core.DomainObjects;
    using BikeDistributor.Core.Enums;
    using System.Linq;
    using System;
    using System.Text;

    public class RecieptGenerator : IRecieptGenerator
    {
        public RecieptGenerator(
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

        public string GenerateReciept(Order order, ReceiptType receiptType)
        {
            if (receiptType == ReceiptType.Plain)
            {
                return _getPlain(order);
            }
            else if (receiptType == ReceiptType.Html)
            {
                return _getHtml(order);
            }
            else
            {
                return "You ask for it - you got it - NO reciept for you!";
            }
        }

        private string _getPlain(Order order)
        {
            var result = new StringBuilder($"Order Receipt for " +
                                           $"{order.Company}{Environment.NewLine}");

            decimal preTaxGross = 0;

            foreach (var line in order.Lines)
            {
                decimal bikeGross = line.Bike.Price * line.Quantity;

                decimal ageDiscountCoefficient = this._discountProvider
                    .IssueAgingDiscount(line.Bike.DaysInInventory);

                decimal bikesTotal = bikeGross - (bikeGross * ageDiscountCoefficient);

                preTaxGross += bikesTotal;

                result.AppendLine(
                    $"{line.Quantity} bikes of brand " +
                    $"{line.Bike.Brand} at " +
                    $"{ageDiscountCoefficient * 100}% age disc at " +
                    $"{line.Bike.Price} each  = a pre-tax total of " +
                    $"{bikesTotal.ToString("C")}");
            }

            decimal volumeDiscountCoefficient = 
                this._discountProvider.IssueVolumeDiscount(preTaxGross);

            decimal preTaxAfterAllDiscountsGross = 
                preTaxGross - (preTaxGross * volumeDiscountCoefficient);

            decimal totalTax = preTaxAfterAllDiscountsGross * _taxRate;

            decimal orderTotal = preTaxAfterAllDiscountsGross + totalTax;

            result.AppendLine($"Order Gross : {preTaxGross.ToString("C")}");

            result.AppendLine($"Less volume discount at {volumeDiscountCoefficient*100} pct : " +
                              $"{(-preTaxGross*volumeDiscountCoefficient).ToString("C")}");
            result.AppendLine($"Pre Tax Amount: {preTaxAfterAllDiscountsGross.ToString("C")}");

            result.AppendLine($"Add Tax at {_taxRate * 100}%: {totalTax.ToString("C")}");

            result.AppendLine($"Order Total {orderTotal.ToString("C")}");

            return result.ToString();
        }

        private string _getHtml(Order order)
        {
            throw new System.NotImplementedException(
                "Same math here other than the html markup string - " +
                "going to skip this sorry got a lil' lazy...");
        }
    }
}