namespace BikeDistributor.Core.DomainObjects
{
    using Contracts;

    public class LineItem : ILineItem
    {
        public LineItem(IInventoryItem inventoryItem, int quantity)
        {
            this.InventoryItem = inventoryItem;
            this.Quantity = quantity;
        }

        public IInventoryItem InventoryItem { get; set; }

        public int Quantity { get; set; }

        public decimal TotalBeforeDiscount { get; set; }

        public decimal DiscountCoefficient { get; set; }

        public decimal DiscountAmount { get; set; }

        public decimal Total { get; set; }

    }
}
