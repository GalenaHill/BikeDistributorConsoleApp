namespace BikeDistributor.Core.DomainObjects
{
    using System.Collections.Generic;
    using System.Linq;
    using Contracts.domainObjects;

    public class LineItem : ILineItem
    {
        public LineItem(IProductInfo productInfoItem, int quantity)
        {
            this.ProductInfoItem = productInfoItem;
            this.Quantity = quantity;
        }

        public IProductInfo ProductInfoItem { get; set; }

        public int Quantity { get; set; }

        public decimal TotalBeforeDiscount { get; set; }

        public decimal DiscountCoefficient { get; set; }

        public decimal DiscountAmount { get; set; }

        public decimal Total { get; set; }

        public ICollection<IDiscountItem> DiscountItems { get; set; } = new List<IDiscountItem>();

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
