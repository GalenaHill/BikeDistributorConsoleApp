namespace BikeDistributor.Core.DomainObjects
{
    using System.Collections.Generic;

    public class Order
    {
        public string Id { get; set; } // need this in real life

        public IList<Line> Lines = new List<Line>();

        public Order(string company)
        {
            Company = company; // this will be an object (entity) in real life
        }

        public string Company { get; set; }

        public void AddLine(Line line)
        {
            Lines.Add(line);
        }

        public decimal Subtotal { get; set; }

        public decimal DiscountCoefficient { get; set; }

        public decimal DiscountAmount { get; set; }

        public decimal SubTotalNetDiscount { get; set; }

        public decimal TaxCoefficient { get; set; }

        public decimal TaxAmount { get; set; }

        public decimal Total { get; set; }



    }
}
