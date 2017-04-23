using BikeDistributor.Core.Contracts;

namespace BikeDistributor.Core.DomainObjects
{
    using System.Collections.Generic;

    public class Order : IOrder
    {
        public Order(ICustomerSalesInfo customerSalesInfo)
        {
            this.CustomerSalesInfo = customerSalesInfo;
        }

        public string Id { get; set; }

        public string CustomerId { get; set; }

        public ICustomerSalesInfo CustomerSalesInfo { get; set; }

        public ICollection<ILineItem> LineItems { get; set; } = new List<ILineItem>();

        public decimal Subtotal { get; set; }

        public decimal ManualDiscountCoefficient { get; set; }

        public decimal DiscountCoefficient { get; set; }

        public decimal DiscountAmount { get; set; }

        public decimal SubTotalNetDiscount { get; set; }

        public decimal TaxCoefficient { get; set; }

        public decimal TaxAmount { get; set; }

        public decimal Total { get; set; }

        public void AddLine(ILineItem lineItem)
        {
            LineItems.Add(lineItem);
        }
    }
}
