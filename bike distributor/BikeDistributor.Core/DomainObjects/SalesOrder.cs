namespace BikeDistributor.Core.DomainObjects
{
    using System.Collections.Generic;
    using System.Linq;
    using Contracts.domainObjects;

    public class SalesOrder : ISalesOrder
    {
        public SalesOrder(ICustomerInfo customerInfo)
        {
            this.CustomerInfo = customerInfo;
        }

        public string Id { get; set; }

        public string CustomerId { get; set; }

        public ICustomerInfo CustomerInfo { get; set; }

        public ICollection<ILineItem> LineItems { get; set; } = new List<ILineItem>();

        public ICollection<IDiscountItem> DiscountItems { get; set; } = new List<IDiscountItem>();

        public decimal Subtotal { get; set; }

        public decimal DiscountCoefficient { get; set; }

        public decimal DiscountAmount { get; set; }

        public decimal SubTotalNetDiscount { get; set; }

        public decimal TaxCoefficient { get; set; }

        public decimal TaxAmount { get; set; }

        public decimal Total { get; set; }

        //
        public void AddLine(ILineItem lineItem)
        {
            LineItems.Add(lineItem);
        }

        public void AddDiscount(IDiscountItem discountItem)
        {
            DiscountItems.Add(discountItem);
        }

        public decimal ManualDiscount { get; set; }

        public decimal GetTotalDiscount()
        {
            return this.DiscountItems.Sum(x => x.DiscountCoefficient) + this.ManualDiscount;
        }
    }
}
