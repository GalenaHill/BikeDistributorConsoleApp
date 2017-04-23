namespace BikeDistributor.Core.DomainObjects
{
    public class Line
    {
        public Line(Bike bike, int quantity)
        {
            Bike = bike;
            Quantity = quantity;
        }

        public Bike Bike { get; private set; }

        public int Quantity { get; set; }

        public decimal TotalBeforeDiscount { get; set; }

        public decimal DiscountCoefficient { get; set; }

        public decimal DiscountAmount { get; set; }

        public decimal Total { get; set; }

    }
}
